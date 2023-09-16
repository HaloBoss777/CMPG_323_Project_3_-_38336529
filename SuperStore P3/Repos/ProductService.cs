using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;

namespace EcoPower_Logistics.Repos
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> FindProductAsync(short? id);

        Task<Product> CreateAProductAsync(Product product);
        Task<Product?> EditProductAsync(short id, Product product);
        Task<Product?> DeleteProductAsync(short id);

    }

    public class ProductService : IProductService
    {
        private readonly SuperStoreContext _superStoreContext;

        public ProductService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }

        //Get All Products
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _superStoreContext.Products.Select(p => new Product() 
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                UnitsInStock = p.UnitsInStock

            }).ToListAsync();
        }

        //Find a Spicific product
        public async Task<Product?> FindProductAsync(short? id)
        {
            var foundRecord = await _superStoreContext.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();

            if(foundRecord is null)
            {
                return null;
            }

            return foundRecord;
        }

        //Create A Product
        public async Task<Product> CreateAProductAsync(Product product)
        {
            await _superStoreContext.AddAsync(product);
            await _superStoreContext.SaveChangesAsync();

            return product;
        }

        //Edit A product
        public async Task<Product?> EditProductAsync(short id, Product product)
        {
            //Returns normal type so can use the find function
            var foundRecord = await FindProductAsync(id);

            if(foundRecord is null)
            {
                return null;
            }

            foundRecord.ProductId = product.ProductId;
            foundRecord.ProductName = product.ProductName;
            foundRecord.ProductDescription = product.ProductDescription;
            foundRecord.UnitsInStock = product.UnitsInStock;

            await _superStoreContext.SaveChangesAsync();

            return foundRecord;
        }

        //Delete A product
        public async Task<Product?> DeleteProductAsync(short id)
        {
            var FoundRecord = await FindProductAsync(id);

            if (FoundRecord is null)
            {
                return null;
            }

            _superStoreContext.Products.Remove(FoundRecord);
            await _superStoreContext.SaveChangesAsync();

            return FoundRecord;
        }
    }
}
