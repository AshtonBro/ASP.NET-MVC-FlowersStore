using FlowersStore.Contracts;
using FlowersStore.Data;
using FlowersStore.Helpers;
using FlowersStore.Services.ServicesInterfaces;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FlowersStore.Controllers
{
    [Authorize]
    [Authorize(Policy = "User")]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._userService = userService;
        }

        public IActionResult Index(ProfileViewModel model)
        {
            var user = _userService.GetUser(HttpContext.User.Identity.Name);
            if (user == null) return new JsonRedirect("Such a user isn't found.");

            model.Id = user.Id;
            model.Role = _userService.GetUserRole(user.UserName);
            model.Name = user.Name;
            model.SecondName = user.SecondName;
            model.Phone = user.PhoneNumber;
            model.Email = user.Email;

            return View(model);
        }

        public JsonRedirect ChangeUserModel(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUser(HttpContext.User.Identity.Name);
                var changedUser = new User()
                {
                    Id = user.Id,
                    Name = model.Name,
                    SecondName = model.SecondName,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    PasswordHash = model.Password
                };

                if (user.Name != changedUser.Name
                    || user.SecondName != changedUser.SecondName
                    || user.Email != changedUser.Email
                    || user.PhoneNumber != changedUser.PhoneNumber)
                {
                    var hashPasswordNew = _userManager.PasswordHasher.HashPassword(user, changedUser.PasswordHash);

                    changedUser.PasswordHash = hashPasswordNew;
                    _userService.UserUpdate(changedUser);

                    _signInManager.RefreshSignInAsync(changedUser);
                    return new JsonRedirect("User successful changed.");
                }
                return new JsonRedirect("Nothing to change.");
            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }
    }
}
