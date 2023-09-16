using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models;

namespace EcoPower_Logistics.Repos
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetAllOrders_Products();
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly SuperStoreContext _superStoreContext;

        public OrderDetailService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }

        public async Task<List<OrderDetail>> GetAllOrders_Products()
        {
            return await _superStoreContext.OrderDetails.Include(o => o.Order).Include(o => o.Product).ToListAsync();
        }

        //public async

    }
}
