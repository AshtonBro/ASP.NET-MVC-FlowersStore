using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.Helpers;
using FlowersStore.WebUI.ViewModels;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class ProfileController : Controller
    {
        private readonly UserManager<DataAccess.MSSQL.Entities.User> _userManager;
        private readonly SignInManager<DataAccess.MSSQL.Entities.User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(
            UserManager<DataAccess.MSSQL.Entities.User> userManager,
            SignInManager<DataAccess.MSSQL.Entities.User> signInManager,
            IUserService userService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            var userNameContext = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var user = await _userService.Get(userNameContext);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userClaim = await _userService.GetUserClaim(user.UserName);

            if (string.IsNullOrEmpty(userClaim))
            {
                throw new ArgumentNullException(nameof(userClaim));
            }

            model.Id = user.Id;
            model.Role = userClaim;
            model.Name = user.Name;
            model.SecondName = user.SecondName;
            model.Phone = user.PhoneNumber;
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

            var userNameContext = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var userCore = await _userService.Get(userNameContext);

            var user = _mapper.Map<Core.CoreModels.User, DataAccess.MSSQL.Entities.User>(userCore);

            var updatedUserCore = new DataAccess.MSSQL.Entities.User()
            {
                Id = user.Id,
                Name = model.Name,
                SecondName = model.SecondName,
                PhoneNumber = model.Phone,
                Email = model.Email,
                PasswordHash = model.Password
            };

            if (user.Name != updatedUserCore.Name
                || user.SecondName != updatedUserCore.SecondName
                || user.Email != updatedUserCore.Email
                || user.PhoneNumber != updatedUserCore.PhoneNumber)
            {

                var hashPasswordNew = _userManager.PasswordHasher.HashPassword(user, updatedUserCore.PasswordHash);

                updatedUserCore.PasswordHash = hashPasswordNew;

                var updatedUserEntity = _mapper.Map<DataAccess.MSSQL.Entities.User, Core.CoreModels.User>(updatedUserCore);

                await _userService.Update(updatedUserEntity);

                var updatedUser = _mapper.Map<Core.CoreModels.User, DataAccess.MSSQL.Entities.User>(updatedUserEntity);

                await _signInManager.RefreshSignInAsync(updatedUser);

                return new JsonRedirect("The user has been updated successfully.");
            }

            return new JsonRedirect("No changes in the fields.");
        }
    }
}