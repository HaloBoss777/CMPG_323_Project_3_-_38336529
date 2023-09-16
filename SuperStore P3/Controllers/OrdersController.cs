using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
using EcoPower_Logistics.Repos;

namespace Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrdersController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var superStoreContext = await _orderService.GetAllOrdersAsync();
            return View(superStoreContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(short? id)
        {

            var order = await _orderService.FindOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList((System.Collections.IEnumerable)_customerService.GetAllCustomersAsync(), "CustomerId", "CustomerId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList((System.Collections.IEnumerable)_customerService.GetAllCustomersAsync(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {

            var order = await _orderService.FindOrderAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList((System.Collections.IEnumerable)_customerService.GetAllCustomersAsync(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var UpdatedOrder = await _orderService.EditOrderAsync(id, order);

                if (UpdatedOrder is null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList((System.Collections.IEnumerable)_customerService.GetAllCustomersAsync(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {

            var order = await _orderService.FindOrderAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {

            var DeletedRecord = _orderService.DeleteOrderAsync(id);

            if (DeletedRecord is null)
            {
                return NotFound();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderExists(short id)
        {
            var foundRecord = await _orderService.FindOrderAsync(id);

            if (foundRecord is null)
            {
                return false;
            }

            return true;
        }
    }
}
