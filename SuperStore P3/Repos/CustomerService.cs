using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repos
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> findCustomerAsync(short? id);
        Task<Customer> CreateCustomerAsync(Customer addCustomer);
        Task<Customer?> EditCustomerAsync(short? id, Customer customer);
        Task<Customer?> DeleteCustomer(short id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly SuperStoreContext _superStoreContext;

        public CustomerService(SuperStoreContext superStoreContext)
        {
            _superStoreContext = superStoreContext;
        }

        //Get All Customers
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

        //Find a Spisific customer
        public async Task<Customer?> findCustomerAsync(short? id)
        {
            if(id is null)
            {
                return null;
            }

            var foundRecord = await _superStoreContext.Customers.Where(x => x.CustomerId == id).FirstOrDefaultAsync();

            if (foundRecord is null)
            {
                return null;
            }

            return foundRecord;
        }

        //Add a customer to the database
        public async Task<Customer> CreateCustomerAsync(Customer addCustomer)
        {

            await _superStoreContext.Customers.AddAsync(addCustomer);
            await _superStoreContext.SaveChangesAsync();

            return addCustomer;
        }

        //Edit Customer
        public async Task<Customer?> EditCustomerAsync(short id, Customer customer)
        {
            var foundCustomer = await findCustomerAsync(id);

            if(foundCustomer is null)
            {
                return null;
            }

            foundCustomer.CustomerId = customer.CustomerId;
            foundCustomer.CustomerName = customer.CustomerName;
            foundCustomer.CustomerSurname = customer.CustomerSurname;
            foundCustomer.CustomerTitle = customer.CustomerTitle;
            foundCustomer.CellPhone = customer.CellPhone;

            await _superStoreContext.SaveChangesAsync();

            return foundCustomer;

        }

        //Delete Customer
        public async Task<Customer?> DeleteCustomer(short id)
        {
            var foundcustomer = await findCustomerAsync(id);

            if (foundcustomer is null)
            {
                return null;
            }

            _superStoreContext.Customers.Remove(foundcustomer);
            await _superStoreContext.SaveChangesAsync();

            return foundcustomer;
        }

    }
}
