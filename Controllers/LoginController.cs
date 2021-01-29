using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;
using FlowersStore.Helpers;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Models;

namespace FlowersStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonRedirect LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (StoreDBContext db = new StoreDBContext())
                {
                    var user = db.Users.FirstOrDefault(f => f.Name == model.LoginUser.Name);
                    if (user == null) return new JsonRedirect("A such user isn't registered.");

                    var isPasswordValid = user.Password == model.LoginUser.Password;
                    if (isPasswordValid)
                    {
                        return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
                    }
                    return new JsonRedirect("Invalid login or password.");
                }
            }

            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

        [HttpPost]
        public JsonRedirect RegistrationUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (StoreDBContext db = new StoreDBContext())
                {
                    var user = db.Users.FirstOrDefault(f => (f.Name == model.RegistrationUser.Name) 
                    || (f.Email == model.RegistrationUser.Email));
                    if (user == null)
                    {
                        User userRegistration = new User()
                        {
                            UserId = Guid.NewGuid(),
                            Name = model.RegistrationUser.Name,
                            SecondName = model.RegistrationUser.SecondName,
                            Phone = model.RegistrationUser.Phone,
                            Email = model.RegistrationUser.Email,
                            Password = model.RegistrationUser.Password,
                            DateCreated = DateTime.Now
                        };

                        db.Users.Add(userRegistration);
                        db.SaveChanges();
                        return new JsonRedirect("You successfully registered. Try to login");
                    }
                    return new JsonRedirect("Such User or Email is registered.");
                }

            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }
    }
}
