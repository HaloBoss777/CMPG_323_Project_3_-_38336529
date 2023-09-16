using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using EcoPower_Logistics.Repos;

namespace Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(short? id)
        {

            var product = await _productService.FindProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,UnitsInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            
            var product = await _productService.FindProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ProductId,ProductName,ProductDescription,UnitsInStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdatedProduct = await _productService.EditProductAsync(id, product);

                if (UpdatedProduct is null) 
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {

            var product = await _productService.FindProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var FoundProduct = await _productService.FindProductAsync(id);

            if (FoundProduct is not null)
            {
                await _productService.DeleteProductAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(short id)
        {
            var foundRecord = await _productService.FindProductAsync(id);

            if (foundRecord is null)
            {
                return false;
            }

            return true;
        }
    }
}
