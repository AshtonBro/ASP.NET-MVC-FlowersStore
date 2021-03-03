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
            
            model.Name = user.Name;
            model.SecondName = user.SecondName;
            model.Phone = user.PhoneNumber;
            model.Email = user.Email;

            return View(model);
        }

        public JsonRedirect ChangeUserModel(ProfileViewModel model)
        {
            if(ModelState.IsValid)
            {
                return new JsonRedirect("User successful changed.");
            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }
    }
}
