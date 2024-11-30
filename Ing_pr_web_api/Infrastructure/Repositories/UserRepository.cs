using Ing_pr_web_api.Application.Interfaces;
using Ing_pr_web_api.Domain.Models;
using Npgsql;

namespace Ing_pr_web_api.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = new List<User>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var command = new NpgsqlCommand("SELECT * FROM users", connection);
            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            FullName = reader.GetString(reader.GetOrdinal("full_name")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("password_hash")),
                            Role = reader.GetString(reader.GetOrdinal("role")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            User user = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM users WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            FullName = reader.GetString(reader.GetOrdinal("full_name")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("password_hash")),
                            Role = reader.GetString(reader.GetOrdinal("role")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                        };
                    }
                }
            }

            return user;
        }

        public async Task AddAsync(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand(@"
                    INSERT INTO users (full_name, email, password_hash, role, created_at)
                    VALUES (@FullName, @Email, @PasswordHash, @Role, @CreatedAt)", connection);

                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand(@"
                    UPDATE users
                    SET full_name = @FullName,
                        email = @Email,
                        password_hash = @PasswordHash,
                        role = @Role,
                        created_at = @CreatedAt
                    WHERE user_id = @UserId", connection);

                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
                command.Parameters.AddWithValue("@UserId", user.UserId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("DELETE FROM users WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
}