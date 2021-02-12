using FlowersStore.Data;
using FlowersStore.Models;
using System;
using System.Linq;

namespace FlowersStore.Services
{
    public class UserService : IUserService
    {
        public static readonly StoreDBContext _context = new StoreDBContext();

        public Guid GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("User name can't be empty.");
            return _context.Users.FirstOrDefault(f => f.Name == userName).Id;
        }
    }
}
