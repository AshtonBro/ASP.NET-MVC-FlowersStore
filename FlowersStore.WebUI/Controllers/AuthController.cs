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
using FlowersStore.WebUI.Validators;
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

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost("login")]
        [AllowAnonymous]
        public async Task<JsonRedirect> SignIn(AuthViewModel model)
        {
            var _validations = new AuthSignInModelValidator();

            var results = _validations.Validate(model.SignIn);

            if (!results.IsValid)
            {
                var failure = results.Errors.FirstOrDefault();

                return new JsonRedirect(failure.ErrorMessage);
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
            var _validations = new AuthSignUpModelValidator();

            var results = _validations.Validate(model.SignUp);

            if (!results.IsValid)
            {
                var failure = results.Errors.FirstOrDefault();

                return new JsonRedirect(failure.ErrorMessage);
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
                PhoneNumber = model.SignUp.PhoneNumber,
                Email = model.SignUp.Email,
                PasswordHash = model.SignUp.Password,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
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