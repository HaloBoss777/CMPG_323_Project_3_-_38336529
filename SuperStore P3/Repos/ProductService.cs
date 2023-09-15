using Data;


namespace EcoPower_Logistics.Repos
{
    public interface IProductService
    {

    }

    public class ProductService : IProductService
    {
        private readonly SuperStoreContext _superStoreContext;

        public ProductService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }



    }
}
