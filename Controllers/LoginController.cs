using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;
using FlowersStore.Helpers;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FlowersStore.Services.ServicesInterfaces;

namespace FlowersStore.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBasketService _basketService;
        private static readonly StoreDBContext _context = new StoreDBContext();
        public IActionResult Index()
        {
            return View();
        }

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IBasketService basketService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._basketService = basketService;
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
                        PasswordHash = model.RegistrationUser.Password,
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

                        await _signInManager.PasswordSignInAsync(userRegistration, model.RegistrationUser.Password, false, false);

                        _basketService.CreateBasket(userId);
                        _context.SaveChanges();
                     
                        return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
                    }
                    return new JsonRedirect(result.Errors.FirstOrDefault().Code);
                }
                return new JsonRedirect("Such User or Email is registered.");
            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

        public async Task<RedirectResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
