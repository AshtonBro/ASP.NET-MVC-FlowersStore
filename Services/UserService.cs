using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
using FlowersStore.ViewModels;
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

        public string GetUserRole(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("User name can't be empty.");
            var userId = GetUser(userName).Id;
            using StoreDBContext _context = new StoreDBContext();
            return _context.UserClaims.FirstOrDefault(f => f.UserId == userId).ClaimValue;
        }

        public bool UserUpdate(User user)
        {
            using StoreDBContext _context = new StoreDBContext();
            User oldModel = _context.Users.FirstOrDefault(f => f.Id == user.Id);
            if (oldModel == null) return false;

            oldModel.Name = user.Name;
            oldModel.UserName = user.Name;
            oldModel.NormalizedUserName = user.Name.ToUpper();
            oldModel.SecondName = user.SecondName;
            oldModel.PhoneNumber = user.PhoneNumber;
            oldModel.Email = user.Email;
            oldModel.NormalizedEmail = user.Email.ToUpper();

            return _context.SaveChanges() >= 1;
        }
    }
}
