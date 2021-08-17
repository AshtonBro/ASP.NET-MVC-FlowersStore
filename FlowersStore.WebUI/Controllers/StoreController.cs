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
    public class StoreController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public StoreController(
            UserManager<User> userManager,
            IProductService productService,
            IMapper mapper)
        {
            _userManager = userManager;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var products = await _productService.Get();

            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var productsView = _mapper.Map<ICollection<Core.CoreModels.Product>, ICollection<ProductViewModel>>(products);

            var model = new StoreViewModel
            {
                UserName = user.Name,
                Products = productsView
            };

            return View(model);
        }
    }
}