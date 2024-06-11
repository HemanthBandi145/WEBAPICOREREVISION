using Microsoft.Data.SqlClient;
using System.Data;
using WebAPIRevision.DTO;
using WebAPIRevision.Models;

namespace WebAPIRevision.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public CustomerRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task DeleteCustomerByIdAsync(int id)
        {
            using (var connection = sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("RemoveCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
            //throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var result = new List<Customer>();
            using (var connection = sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Exec GetAllCustomers", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Customer customer = new Customer();
                            customer.CustomerId = reader.GetInt32("CustomerId");
                            customer.Name = reader.GetString("Name");
                            customer.Email = reader.GetString("Email");
                            customer.Address = reader.GetString("Address");
                            customer.IsDeleted = false;
                            result.Add(customer);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            Customer? customer = new Customer();
            using (var connection = sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetCustomerById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            //customer = new Customer();
                            customer.Name = reader.GetString("Name");
                            customer.Address = reader.GetString("Address");
                            customer.Email = reader.GetString("Email");
                            customer.IsDeleted = false;
                        }
                    }
                }
            }
            return customer;
        }

        public async Task UpdateCustomer(CustomerDTO customer)
        {
            using (var connection = sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId",customer.CustomerId);
                    command.Parameters.AddWithValue("@CustomerName", customer.Name);
                    command.Parameters.AddWithValue("@CustomerEmail", customer.Email);
                    command.Parameters.AddWithValue("@CustomerAddress", customer.Address);
                    await command.ExecuteNonQueryAsync();
                }
            }
            
        }
    }
}
