using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("customer")]
    [Route("customer")]
    [Route("customer/order")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
      
        //GET Checkout actioin method
        [Route("checkout")]
        public IActionResult Checkout()
        {
            return View();
        }
      
        //POST Checkout action method

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("checkout")]
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products!=null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails=new OrderDetails();
                    orderDetails.PorductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNo = GetOrderNo();
            _db.Orders.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }

        [Route("getorderno")]
        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count()+1;
            return rowCount.ToString("000");
        }
    }
}
