using System.Reflection;
using HomeBudget.Integration;
using HomeBudget.Integration.Logging;
using HomeBudget.MonthBudget.API.Integration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scrutor;

namespace HomeBudget.MonthBudget.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MonthBudget.Infrastructure.MonthBudgetContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Default"));
                o.UseLazyLoadingProxies();
            });

            services.AddDbContext<HomeBudget.Integration.Logging.IntegrationEventLogContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Integration"));
                o.UseLazyLoadingProxies();
            });
            
            services.AddLogging(config =>
            {
                config.AddConsole();
            });
            
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .Scan(s =>
                {
                    s
                        .FromAssemblies(
                            typeof(HomeBudget.MonthBudget.Infrastructure.IAssemblyMarker).Assembly
                            )
                        .AddClasses(c => c.Where(x => 
                            x.Name.EndsWith("Repository")
                        ))
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithTransientLifetime();

                    s.FromAssemblies(typeof(HomeBudget.MonthBudget.Infrastructure.IAssemblyMarker).Assembly)
                        .AddClasses(c => c.Where(t => t.Name.Contains("Context") || t.Name.Contains("Repository")))
                        .AsSelf()
                        .WithScopedLifetime();

                    s.AddTypes<IIntegrationService, MonthBudgetIntegrationService>().AsImplementedInterfaces().WithTransientLifetime();
                    s.AddTypes<IEventBus, EventServiceBus>().AsImplementedInterfaces().WithTransientLifetime();

                    s.FromAssemblies(typeof(HomeBudget.Integration.IAssemblyMarker).Assembly)
                        .AddClasses(c => c.Where(t => t.Name.Contains("Integration")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
                        
                    s.FromAssemblies(typeof(Startup).Assembly)
                        .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
                        .AsSelf()
                        .WithTransientLifetime();
                })
                .AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            
            loggerFactory.CreateLogger<Startup>().LogDebug("Logger on", Configuration["PATH_BASE"]);
            app.UsePathBase(Configuration["PATH_BASE"]);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
