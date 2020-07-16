using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorgianComputers.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeorgianComputers.Controllers
{
    public class ShopController : Controller
    {
        //Add database connection
        private readonly GeorgianComputersContext _context;

        public ShopController(GeorgianComputersContext context)
        {
            _context = context;
        }

        // GET Shop
        public IActionResult Index()
        {
            // return list of categories for the user to browse 
            var categories = _context.Category.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        public IActionResult Browse(String category)
        {
            //Store the selected category in the viewbag
            ViewBag.Category = category;

            //Get the list of products for the selected category and pass the list to the view
            var products = _context.Product.Where(p => p.Category.Name == category).OrderBy(p => p.Name).ToList();
            return View(products);
        }

        public IActionResult ProductDetails(string product)
        {
            // Use a SingleOrDefault to find either 1 exact match or a null object
            var selectedProduct = _context.Product.SingleOrDefault(p => p.Name == product);
            return View(selectedProduct);
        }

        //POST: AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int Quantity, int ProductId)
        {
            //Identify product price
            var product = _context.Product.SingleOrDefault(p => p.ProductId == ProductId);
            var price = product.Price;

            //Create and save new cart object
            var cart = new Cart
            {
                ProductId = ProductId,
                Quantity = Quantity,
                Price = price,
                Username = "tempUser"
            };

            _context.Cart.Add(cart);
            _context.SaveChanges();

            return RedirectToAction("Cart");
        }
    }
}
