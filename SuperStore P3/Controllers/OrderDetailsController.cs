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
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderDetailsController(IOrderDetailService OrderDetailService, IOrderService orderService, IProductService productService)
        {
            _orderDetailService = OrderDetailService;
            _orderService = orderService;
            _productService = productService;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var superStoreContext = _orderDetailService.GetAllOrders_Products();
            return View(await superStoreContext);
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(short? id)
        {

            var orderDetail = await _orderDetailService.findOrderDetailsAsync(id);

            if (orderDetail is null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await _orderService.GetAllOrdersAsync(), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(await _productService.GetAllProductsAsync(), "ProductId", "ProductId");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailService.CreateOrderDetailAsync(orderDetail);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(await _orderService.GetAllOrdersAsync(), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(await _productService.GetAllProductsAsync(), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(short? id)
        { 
            var orderDetail = await _orderDetailService.findOrderDetailsAsync(id);
            if (orderDetail is null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(await _orderService.GetAllOrdersAsync(), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(await _productService.GetAllProductsAsync(), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                var updatedOrderDetail = await _orderDetailService.EditOrderDetailAsync(id ,orderDetail);

                if (updatedOrderDetail is null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(await _orderService.GetAllOrdersAsync(), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(await _productService.GetAllProductsAsync(), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {

            var orderDetail = await _orderDetailService.findOrderDetailsAsync(id);

            if (orderDetail is null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {

            var orderDetail = await _orderDetailService.findOrderDetailsAsync(id);

            if (orderDetail is not null)
            {
                await _orderDetailService.DeleteOrderDetailAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderDetailExists(short id)
        {
            var foundRecord = await _orderDetailService.findOrderDetailsAsync(id);

            if(foundRecord is null)
            {
                return false;
            }

            return true;
        }
    }
}
