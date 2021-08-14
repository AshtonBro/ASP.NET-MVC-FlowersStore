using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class BasketController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IShopingCartService _shopingCartService;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(
            UserManager<User> userManager,
            IShopingCartService shopingCartservice,
            IBasketService basketService,
            IMapper mapper)
        {
            _userManager = userManager;
            _shopingCartService = shopingCartservice;
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var basket = await _basketService.Get(user.Name);

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

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _shopingCartService.CreateOrUpdate(user.Name, productId, quantity);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }

        public async Task<JsonResult> DeleteAllFromBasket()
        {
            var user = await _userManager.GetUserAsync(User);

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

