using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using FlowersStore.WebUI.Helpers;
using FlowersStore.DataAccess.MSSQL.Entities;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public IActionResult Index()
        {
            return View();
        }

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IBasketService basketService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
            _mapper = mapper;
        }

        //[HttpPost("login")]
        [AllowAnonymous]
        public async Task<JsonRedirect> SignIn(AuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();

                return new JsonRedirect(error.ErrorMessage);
            }

            var user = await _userManager.FindByNameAsync(model.SignIn.Name);

            if (user == null)
            {
                return new JsonRedirect("User is not registered.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.SignIn.Password, false, false);

            if (!result.Succeeded)
            {
                return new JsonRedirect("Login or password is invalid.");
            }

            return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
        }

        //[HttpPost("registration")]
        [AllowAnonymous]
        public async Task<JsonRedirect> SignUp(AuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();

                return new JsonRedirect(error.ErrorMessage);
            }

            var user = await _userManager.FindByNameAsync(model.SignUp.Name);

            if (user != null)
            {
                return new JsonRedirect("The user is already registered.");
            }

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = model.SignUp.Name,
                Name = model.SignUp.Name,
                SecondName = model.SignUp.SecondName,
                PhoneNumber = model.SignUp.Phone,
                Email = model.SignUp.Email,
                PasswordHash = model.SignUp.Password,
                DateCreated = DateTime.Now
            };

            var result = await _userManager.CreateAsync(newUser, model.SignUp.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, ClaimPolicyMatch.USER));

                await _signInManager.PasswordSignInAsync(newUser, model.SignUp.Password, false, false);

                await _basketService.Create(newUser.Id);

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