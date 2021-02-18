using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
using System;
using System.Linq;

namespace FlowersStore.Services
{
    public class UserService : IUserService
    {
        public User GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("User name can't be empty.");
            using StoreDBContext _context = new StoreDBContext();
            return _context.Users.FirstOrDefault(f => f.Name == userName);
        }
    }
}
