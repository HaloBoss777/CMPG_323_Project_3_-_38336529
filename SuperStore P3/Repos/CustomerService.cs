using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repos
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
    }

    public class CustomerService : ICustomerService
    {
        private readonly SuperStoreContext _superStoreContext;

        public CustomerService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _superStoreContext.Customers.Select(x => new Customer
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                CellPhone = x.CellPhone,
                CustomerTitle = x.CustomerTitle,

            }).ToListAsync();
        }

    }
}
