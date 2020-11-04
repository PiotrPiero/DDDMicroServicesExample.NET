using HomeBudget.Core.Impl;
using System;

namespace HomeBudget.Core
{
    public class AppUser : Entity
    {

        public AppUser() : base() { }

        public AppUser(int id, DateTime created, DateTime modified, int authorId, string login, string firstName, string lastName) : base(id, created, modified, authorId)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
