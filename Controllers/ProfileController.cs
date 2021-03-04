using FlowersStore.Data;
using FlowersStore.Helpers;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
using FlowersStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private static readonly StoreDBContext _context = new StoreDBContext();
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
            model.Role = _userService.GetUserRole(user.Name);
            model.Name = user.Name;
            model.SecondName = user.SecondName;
            model.Phone = user.PhoneNumber;
            model.Email = user.Email;

            return View(model);
        }

        public async Task<JsonRedirect> ChangeUserModel(ProfileViewModel model)
        {
            if(ModelState.IsValid)
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


                var result = _signInManager.PasswordSignInAsync(user.Name, changedUser.PasswordHash, false, false)
                    .GetAwaiter()
                    .GetResult();

                if (result.Succeeded)
                {
                    _userService.UserUpdate(changedUser);
                    await _signInManager.SignOutAsync();
                    return new JsonRedirect(new Link(nameof(HomeController), nameof(HomeController.Index)));
                    //return new JsonRedirect("User successful changed.");
                }

                return new JsonRedirect("Incorrect password.");
            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }
    }
}
