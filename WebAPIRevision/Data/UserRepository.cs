using Microsoft.Data.SqlClient;
using System.Data;
using WebAPIRevision.Models;

namespace WebAPIRevision.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public UserRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Users> GetUserbyUsernameAsync(string Username)
        {
            Users users = new Users();
            using (var connection = sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetUserByUserName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", Username);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            users.UserName = reader.GetString("UserName");
                            users.Password = reader.GetString("Password");
                            users.Role = reader.GetString("Role");
                        }
                    }
                }
            }
            return users;
        }

        public async Task<int> CreateNewUser(Users users)
        {
            using (var connection = sqlConnectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddNewUser",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", users.UserName);
                    command.Parameters.AddWithValue("@Password", users.Password);
                    command.Parameters.AddWithValue("@Role", users.Role);

                    int result = (int)await command.ExecuteNonQueryAsync();
                    return result;
                }
            }
        }

    }
}
