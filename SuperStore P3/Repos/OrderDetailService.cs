using Data;


namespace EcoPower_Logistics.Repos
{
    public interface IOrderDetailService
    {

    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly SuperStoreContext _superStoreContext;

        public OrderDetailService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }



    }
}
