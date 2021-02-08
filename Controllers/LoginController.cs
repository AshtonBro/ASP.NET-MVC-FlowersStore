using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;
using FlowersStore.Helpers;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Models;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FlowersStore.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        Crypto myCryptoHasher = new Crypto();
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private static readonly StoreDBContext _context = new StoreDBContext();
        public IActionResult Index()
        {
            return View();
        }

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonRedirect> LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.LoginUser.Name);
                if (user == null) return new JsonRedirect("A such user isn't registered.");

                var result = await _signInManager.PasswordSignInAsync(user, model.LoginUser.Password, false, false);

                if (result.Succeeded)
                {
                    return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
                }
                return new JsonRedirect("Invalid login or password.");
            }

            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonRedirect> RegistrationUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.RegistrationUser.Name);
                var userId = Guid.NewGuid();
                if (user == null)
                {
                    User userRegistration = new User()
                    {
                        Id = userId,
                        UserName = model.RegistrationUser.Name,
                        Name = model.RegistrationUser.Name,
                        SecondName = model.RegistrationUser.SecondName,
                        PhoneNumber = model.RegistrationUser.Phone,
                        Email = model.RegistrationUser.Email,
                        Password = model.RegistrationUser.Password,
                        DateCreated = DateTime.Now
                    };

                    Basket newBasketForUser = new Basket()
                    {
                        BasketId = Guid.NewGuid(),
                        Id = userId,
                        DateCreated = DateTime.Now

                    };

                    var result = _userManager.CreateAsync(userRegistration, model.RegistrationUser.Password)
                        .GetAwaiter()
                        .GetResult();

                    if (result.Succeeded)
                    {
                        _userManager.AddClaimAsync(userRegistration, new Claim(ClaimTypes.Role, "User"))
                           .GetAwaiter()
                           .GetResult();

                        _context.Baskets.Add(newBasketForUser);
                        _context.SaveChanges();
                        return new JsonRedirect("You successfully registered. Try to login");
                    }
                    return new JsonRedirect(result.Errors.FirstOrDefault().Code);
                }
                return new JsonRedirect("Such User or Email is registered.");

            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return View("~/Views/Home/Index.cshtml");
        }

    }
}
