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

        public bool UserUpdate(ProfileViewModel model)
        {
            using StoreDBContext _context = new StoreDBContext();
            User oldModel = _context.Users.FirstOrDefault(f => f.Id == model.Id);
            if (oldModel == null) return false;

            oldModel.Name = model.Name;
            oldModel.SecondName = model.SecondName;
            oldModel.PhoneNumber = model.Phone;
            oldModel.Email = model.Email;
            oldModel.UserName = model.Name;

            return _context.SaveChanges() >= 1;
        }
    }
}
