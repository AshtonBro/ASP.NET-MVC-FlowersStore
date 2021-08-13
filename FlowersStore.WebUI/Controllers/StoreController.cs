using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FlowersStore.Core.Services;
using FlowersStore.WebUI.ViewModels;
using System.Collections.Generic;
using AutoMapper;

namespace FlowersStore.WebUI.Controllers
{
    [Authorize]
    [Authorize(Policy = ClaimPolicyMatch.USER)]
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public StoreController(
            IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userNameContext = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var products = await _productService.Get();

            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var productsView = _mapper.Map<ICollection<Core.CoreModels.Product>, ICollection<ProductViewModel>>(products);

            var model = new StoreViewModel
            {
                UserName = userNameContext,
                Products = productsView
            };

            return View(model);
        }
    }
}