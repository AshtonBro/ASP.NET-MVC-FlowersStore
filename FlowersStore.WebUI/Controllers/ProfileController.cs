using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.WebUI.Helpers;
using FlowersStore.WebUI.ViewModels;
using FlowersStore.DataAccess.MSSQL.Entities;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ProfileController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(ProfileViewModel model)
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

        public async Task<JsonRedirect> UpdateUserModel(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
                return new JsonRedirect(error.ErrorMessage);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Name != model.Name
                || user.SecondName != model.SecondName
                || user.Email != model.Email
                || user.PhoneNumber != model.PhoneNumber)
            {
                var passwordHashNew = _userManager.PasswordHasher.HashPassword(user, model.Password);

                user.Name = model.Name;
                user.NormalizedUserName = model.Name.ToUpper();
                user.UserName = model.Name;
                user.SecondName = model.SecondName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.PasswordHash = passwordHashNew;
                user.UpdatedDate = DateTime.Now;

                var updatedUser = await _userManager.UpdateAsync(user);

                await _signInManager.RefreshSignInAsync(user);

                return new JsonRedirect("The user has been updated successfully.");
            }

            return new JsonRedirect("No changes in the fields.");
        }
    }
}