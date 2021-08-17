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
        private readonly IProductCardService _productCardservice;
        private readonly IMapper _mapper;

        public CheckoutController(
            UserManager<User> userManager,
            IProductCardService productCardservice,
            IMapper mapper)
        {
            _userManager = userManager;
            _productCardservice = productCardservice;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var productCards = await _productCardservice.GetAllByUserId(user.Id);

            if (productCards == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var productCardsView = _mapper.Map<ICollection<Core.CoreModels.ProductCard>, ICollection<ProductCardViewModel>>(productCards);

            var model = new CheckoutViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                ProductCards = productCardsView
            };

            return View("~/Views/Checkout/Index.cshtml", model);
        }
    }
}