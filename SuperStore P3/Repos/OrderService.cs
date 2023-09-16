using Data;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repos
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> FindOrderAsync(short? id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> EditOrderAsync(short id, Order order);
        Task<Order?> DeleteOrderAsync(short id);
    }

    public class OrderService : IOrderService
    {
        private readonly SuperStoreContext _superStoreContext;

        public OrderService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }

        //Get all Orders and The customer of the order
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _superStoreContext.Orders.Include(o => o.CustomerId).ToListAsync();
        }

        public async Task<Order?> FindOrderAsync(short? id)
        {
            if (id is null)
            {
                return null;
            }

            var foundRecord = await _superStoreContext.Orders.Include(o => o.Customer).FirstOrDefaultAsync(m => m.OrderId == id);

            if (foundRecord is null)
            {
                return null;
            }

            return foundRecord;
        }

        //Add a new Order
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _superStoreContext.Orders.AddAsync(order);
            await _superStoreContext.SaveChangesAsync();

            return order;
        }

        //Edit a order
        public async Task<Order?> EditOrderAsync(short id, Order order)
        {
            //Need base type not wraped in a task so need cannot call find function
            var foundRecord = await _superStoreContext.Orders.Include(o => o.Customer).FirstOrDefaultAsync(m => m.OrderId == id);

            if (foundRecord is null) 
            {
                return null;
            }

            foundRecord.OrderId = order.OrderId;
            foundRecord.OrderDate = order.OrderDate;
            foundRecord.CustomerId = order.CustomerId;
            foundRecord.DeliveryAddress = order.DeliveryAddress;

            await _superStoreContext.SaveChangesAsync();

            return foundRecord;
            
        }

        //Delete a order
        public async Task<Order?> DeleteOrderAsync(short id)
        {
            var foundRecord = await FindOrderAsync(id);

            if (foundRecord is null)
            {
                return null;
            }

            _superStoreContext.Orders.Remove(foundRecord);
            await _superStoreContext.SaveChangesAsync();

            return foundRecord;

        }
    }
}
