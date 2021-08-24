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
        private readonly IProductCardService _productCardService;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(
            UserManager<User> userManager,
            IProductCardService productCardservice,
            IBasketService basketService,
            IMapper mapper)
        {
            _userManager = userManager;
            _productCardService = productCardservice;
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var basket = await _basketService.Get(user.Name);

            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var productCardsView = _mapper.Map<ICollection<Core.CoreModels.ProductCard>, ICollection<ProductCardViewModel>>(basket.ProductCards);

            var model = new BasketViewModel
            {
                Name = basket.User.Name,
                UserLogin = basket.User.UserName,
                ProductCards = productCardsView
            };

            return View("~/Views/Basket/Index.cshtml", model);
        }

        public async Task<JsonResult> DeleteFromBasket(Guid productCardId)
        {
            if (productCardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCardId));
            }

            var result = await _productCardService.Delete(productCardId);

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

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _productCardService.CreateOrUpdate(user.Name, productId, quantity);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }

        public async Task<JsonResult> DeleteAllFromBasket()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _productCardService.DeleteAllByUserId(user.Id);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }

        public async Task<JsonResult> ChangeProductCardQuantity(Guid productCardId, int quantity)
        {
            if (productCardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCardId));
            }

            if (quantity < 0)
            {
                throw new ArgumentNullException(nameof(quantity));
            }

            var exisingProductCard = await _productCardService.Get(productCardId);

            if (exisingProductCard == null)
            {
                throw new ArgumentNullException(nameof(exisingProductCard));
            }

            exisingProductCard.Quantity = quantity;

            var result = await _productCardService.Update(exisingProductCard);

            if (!result)
            {
                return new JsonResult(new { error = "Failure." });
            }

            return new JsonResult(new { message = "Success." });
        }
    }
}