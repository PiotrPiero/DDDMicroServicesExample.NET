using HomeBudget.Account.Infrastructure;
using HomeBudget.Integration;
using HomeBudget.Integration.FinOperationEvents;
using HomeBudget.MonthBudget.API.Integration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Scrutor;

namespace HomeBudget.Account.API
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
            services.AddDbContext<AccountContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Default"));
                o.UseLazyLoadingProxies();
            });

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddSingleton<IEventBus, EventServiceBus>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<EventServiceBus>>();
                
                return new EventServiceBus(logger, Configuration["rabbit_queue"]);
            });
            
            services
                .AddMediatR(typeof(Startup))
                .Scan(s =>
                {
                    s.AddTypes<IIntegrationService, MonthBudgetIntegrationService>().AsImplementedInterfaces().WithScopedLifetime();
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
                    
                    s.FromAssemblies(typeof(HomeBudget.Integration.IAssemblyMarker).Assembly)
                        .AddClasses(c => c.Where(t => t.Name.Contains("Integration")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
                        
                    s.FromAssemblies(typeof(Startup).Assembly)
                        .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
                        .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
                        .AddClasses(c => c.AssignableTo(typeof(IRequest)))
                        .AsSelf()
                        .WithTransientLifetime();
                })
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            
            ConfigureEventBus(app);
        }
        
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<FinOperationAddedIntegrationEvent, IIntegrationEventHandler<FinOperationAddedIntegrationEvent>>();
        }
    }
}
