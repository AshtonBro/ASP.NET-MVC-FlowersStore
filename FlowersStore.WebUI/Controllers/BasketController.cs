using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class BasketController : Controller
    {
        private readonly IShopingCartService _shopingCartService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BasketController(
            IShopingCartService shopingCartservice,
            IBasketService basketService,
            IUserService userService,
            IMapper mapper)
        {
            _shopingCartService = shopingCartservice;
            _basketService = basketService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userNameContext = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var basket = await _basketService.Get(userNameContext);

            if (basket is null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var shopingCartsView = _mapper.Map<ICollection<Core.CoreModels.ShopingCart>, ICollection<ShopingCartViewModel>>(basket.ShopingCarts);

            var model = new BasketViewModel
            {
                Name = basket.User.Name,
                UserLogin = basket.User.UserName,
                ShopingCarts = shopingCartsView
            };

            return View("~/Views/Basket/Index.cshtml", model);
        }

        public async Task<JsonResult> DeleteFromBasket(Guid shopingCartId)
        {
            if (shopingCartId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shopingCartId));
            }

            var result = await _shopingCartService.Delete(shopingCartId);

            if (!result)
            {
                return new JsonResult( new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }

        public async Task<JsonResult> AddToBasket(Guid productId, int quantity)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            if (quantity < 0)
            {
                throw new ArgumentNullException(nameof(quantity));
            }

            var userNameContext = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var result = await _shopingCartService.CreateOrUpdate(userNameContext, productId, quantity);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }

        public async Task<JsonResult> DeleteAllFromBasket(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            var user = await _userService.Get(userName);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _shopingCartService.DeleteAllByUserId(user.Id);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }

        public async Task<JsonResult> ChangeShopingCartQuantity(Guid shopingCartId, int quantity)
        {
            if (shopingCartId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shopingCartId));
            }

            if (quantity < 0)
            {
                throw new ArgumentNullException(nameof(quantity));
            }

            var exisingShopingCart = await _shopingCartService.Get(shopingCartId);

            if (exisingShopingCart is null)
            {
                throw new ArgumentNullException(nameof(exisingShopingCart));
            }

            exisingShopingCart.Quantity = quantity;

            var result = await _shopingCartService.Update(exisingShopingCart);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }
    }
}

