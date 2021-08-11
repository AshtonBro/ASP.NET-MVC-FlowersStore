using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FlowersStore.WebUI.Contracts;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using FlowersStore.WebUI.Helpers;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBasketService _basketService;

        public IActionResult Index()
        {
            return View();
        }

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IBasketService basketService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonRedirect> LoginUser(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();

                return new JsonRedirect(error.ErrorMessage);
            }

            var user = await _userManager.FindByNameAsync(model.LoginUser.Name);

            if (user == null)
            {
                return new JsonRedirect("You are not registered.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.LoginUser.Password, false, false);

            if (result.Succeeded)
            {
                return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
            }

            return new JsonRedirect("Login or password is invalid.");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonRedirect> RegistrationUser(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();

                return new JsonRedirect(error.ErrorMessage);
            }

            var user = await _userManager.FindByNameAsync(model.RegistrationUser.Name);

            if (user == null)
            {
                return new JsonRedirect("The user is already registered.");
            }

            var userRegistration = new User()
            {
                Id = Guid.NewGuid(),
                UserName = model.RegistrationUser.Name,
                Name = model.RegistrationUser.Name,
                SecondName = model.RegistrationUser.SecondName,
                PhoneNumber = model.RegistrationUser.Phone,
                Email = model.RegistrationUser.Email,
                PasswordHash = model.RegistrationUser.Password,
                DateCreated = DateTime.Now
            };

            var result = await _userManager.CreateAsync(userRegistration, model.RegistrationUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(userRegistration, new Claim(ClaimTypes.Role, ClaimPolicyMatch.USER));

                await _signInManager.PasswordSignInAsync(userRegistration, model.RegistrationUser.Password, false, false);

                await _basketService.Create(userRegistration.Id);

                //await _context.SaveChanges();

                return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
            }

            return new JsonRedirect(result.Errors.FirstOrDefault().Code);
        }

        public async Task<RedirectResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/Home/Index");
        }
    }
}