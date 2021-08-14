using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using FlowersStore.DataAccess.MSSQL.Entities;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class CheckoutController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IShopingCartService _shopingCartservice;
        private readonly IMapper _mapper;

        public CheckoutController(
            UserManager<User> userManager,
            IShopingCartService shopingCartservice,
            IMapper mapper)
        {
            _userManager = userManager;
            _shopingCartservice = shopingCartservice;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var shopingCarts = await _shopingCartservice.GetAllByUserId(user.Id);

            if (shopingCarts is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var shopingCartsView = _mapper.Map<ICollection<Core.CoreModels.ShopingCart>, ICollection<ShopingCartViewModel>>(shopingCarts);

            var model = new CheckoutViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                ShopingCarts = shopingCartsView
            };

            return View("~/Views/Checkout/Index.cshtml", model);
        }
    }
}