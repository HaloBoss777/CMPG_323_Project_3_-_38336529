using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models;

namespace EcoPower_Logistics.Repos
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetAllOrders_Products();
        Task<OrderDetail?> findOrderDetailsAsync(short? id);
        Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail);
        Task<OrderDetail?> EditOrderDetailAsync(short id, OrderDetail orderDetail);
        Task<OrderDetail?> DeleteOrderDetailAsync(short id);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly SuperStoreContext _superStoreContext;

        public OrderDetailService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }

        //Get all OrderDetails with orders and products included
        public async Task<List<OrderDetail>> GetAllOrders_Products()
        {
            return await _superStoreContext.OrderDetails.Include(o => o.Order).Include(o => o.Product).ToListAsync();
        }

        //Find a Spisific OrderDetail
        public async Task<OrderDetail?> findOrderDetailsAsync(short? id)
        {
            if (id is null)
            {
                return null;
            }

            var foundRecord = await _superStoreContext.OrderDetails.Include(o => o.Order).Include(o => o.Product).FirstOrDefaultAsync(m => m.OrderDetailsId == id);

            if (foundRecord is null)
            {
                return null;
            }

            return foundRecord;
        }

        //Create OrderDetails
        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            await _superStoreContext.OrderDetails.AddAsync(orderDetail);
            await _superStoreContext.SaveChangesAsync();

            return orderDetail;
        }

        //Edit OrderDetail
        public async Task<OrderDetail?> EditOrderDetailAsync(short id, OrderDetail orderDetail)
        {
            //Need base type not wraped in a task so cannot call find function
            var foundOrder = await _superStoreContext.OrderDetails.Include(o => o.Order).Include(o => o.Product).FirstOrDefaultAsync(m => m.OrderDetailsId == id);

            if (foundOrder is null)
            {
                return null;
            }

            foundOrder.OrderDetailsId = orderDetail.OrderDetailsId;
            foundOrder.OrderId = orderDetail.OrderId;
            foundOrder.ProductId = orderDetail.ProductId;
            foundOrder.Quantity = orderDetail.Quantity;
            foundOrder.Discount = orderDetail.Discount;

            await _superStoreContext.SaveChangesAsync();

            return foundOrder;

        }

        //Delete OrderDetail
        public async Task<OrderDetail?> DeleteOrderDetailAsync(short id)
        {
            var foundRecord = await findOrderDetailsAsync(id);

            if (foundRecord is null)
            {
                return null;
            }

            _superStoreContext.OrderDetails.Remove(foundRecord);
            await _superStoreContext.SaveChangesAsync();

            return foundRecord;
        }
    }
}
