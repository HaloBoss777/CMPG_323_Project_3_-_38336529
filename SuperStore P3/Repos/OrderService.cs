using Data;


namespace EcoPower_Logistics.Repos
{
    public interface IOrderService
    {

    }

    public class OrderService : IOrderService
    {
        private readonly SuperStoreContext _superStoreContext;

        public OrderService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }



    }
}
