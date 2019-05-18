using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FinalProjectMVC.Domain;
using FinalProjectMVC.Services;
using FinalProjectMVC.WebApplicationMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalProjectMVC.WebApplicationMVC.Controllers
{
    [Route("[Controller]")]
    public class OrderController : Controller
    {
        private readonly ShopRepository shopRepository;

        public OrderController()
        {
            shopRepository = new ShopRepository();
        }

        [Route("{Id}")]
        public async Task<IActionResult> Index(Guid Id)
        {
            ProductsViewModel productsViewModel = new ProductsViewModel();

            // load products and categories from database
            productsViewModel.Id = Id;
            productsViewModel.Products = await shopRepository.GetProductsAsync();
            productsViewModel.Categories = await shopRepository.GetCategoriesAsync();

            return View(productsViewModel);
        }

        [HttpPost]
        [Route("Register")]
        // get an array of the ids of the selected products and user id
        public async Task<IActionResult> Register(string[] buy, string[] buyAmount, Guid Id)
        {
            // if no items where selected, error
            if (buy.Count() == 0)   return View();

            // Convert amounts to same size
            buyAmount = buyAmount.Where(b => b != null).ToArray();

            // add order to customer
            Order order = await shopRepository.AddOrderToCustomerAsync(buy, buyAmount, Id);
            return RedirectToAction("View", "Order", new { OrderId = order.ExposeId });
        }

        [HttpGet]
        [Route("View")]
        public async Task<IActionResult> View(Guid OrderId)
        {
            return View(await shopRepository.GetOrderByExposeIdAsync(OrderId));
        }
    }
}