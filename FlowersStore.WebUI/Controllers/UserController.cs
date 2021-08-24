using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.WebUI.Helpers;
using FlowersStore.WebUI.ViewModels;
using FlowersStore.DataAccess.MSSQL.Entities;
using FlowersStore.WebUI.Validators;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(UserViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
                
            if (userClaims.Count == 0)
            {
                throw new ArgumentNullException(nameof(userClaims));
            }

            var userClaim = userClaims.FirstOrDefault(f => f.Value == ClaimPolicyMatch.USER);

            model.Id = user.Id;
            model.Role = userClaim.Value;
            model.Name = user.Name;
            model.SecondName = user.SecondName;
            model.PhoneNumber = user.PhoneNumber;
            model.Email = user.Email;

            return View(model);
        }

        public async Task<JsonRedirect> UpdateUserModel(AuthViewModel.SignUpModel model)
        {
            var _validations = new AuthSignUpModelValidator();

            var results = _validations.Validate(model);

            if (!results.IsValid)
            {
                var failure = results.Errors.FirstOrDefault();

                return new JsonRedirect(failure.ErrorMessage);
            }

            var userViewModel = _mapper.Map<AuthViewModel.SignUpModel, UserViewModel>(model);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Name != userViewModel.Name
                || user.SecondName != userViewModel.SecondName
                || user.Email != userViewModel.Email
                || user.PhoneNumber != userViewModel.PhoneNumber)
            {
                var passwordHashNew = _userManager.PasswordHasher.HashPassword(user, userViewModel.Password);

                user.Name = userViewModel.Name;
                user.NormalizedUserName = userViewModel.Name.ToUpper();
                user.UserName = userViewModel.Name;
                user.SecondName = userViewModel.SecondName;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.Email = userViewModel.Email;
                user.NormalizedEmail = userViewModel.Email.ToUpper();
                user.PasswordHash = passwordHashNew;
                user.UpdatedDate = DateTime.Now;

                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    var failure = updateResult.Errors.FirstOrDefault().Description;

                    return new JsonRedirect(failure);
                }

                await _signInManager.RefreshSignInAsync(user);

                return new JsonRedirect("The user has been updated successfully.");
            }
            else
            {
                return new JsonRedirect("No changes in the fields.");
            }
        }
    }
}