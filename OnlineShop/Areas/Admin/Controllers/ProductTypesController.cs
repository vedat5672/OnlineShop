using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/productTypes")]

    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [Route("Index")]
        public IActionResult Index()
        {
            //var data = _db.ProductTypes.ToList();
            return View(_db.ProductTypes.ToList());
        }

        //GET Create Action Method
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if(ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

        //GET Edit Action Method
        [Route("edit")]
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if(productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }


        //GET Details Action Method
        [Route("details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("details")]
        public  IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
            
        }

        //GET Delete Action Method
        [Route("delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete")]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if(id==null)
            {
                return NotFound();
            }

            if(id!=productTypes.Id)
            {
                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if(productType==null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

    }
}