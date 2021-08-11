using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class CheckoutController : Controller
    {
        private readonly HttpContext _httpContext;
        private readonly IShopingCartService _shopingCartservice;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CheckoutController(
            HttpContext httpContext,
            IShopingCartService shopingCartservice,
            IUserService userService,
            IMapper mapper)
        {
            _httpContext = httpContext;
            _shopingCartservice = shopingCartservice;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userNameContext = _httpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var user = await _userService.Get(userNameContext);

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

