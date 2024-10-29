// File: Repositories/SchoolsRepository.cs

using BookExchangeApi.Models;
using Npgsql;

namespace BookExchangeApi.Repositories
{
    public class SchoolsRepository
    {
        private readonly string _connectionString;

        public SchoolsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateSchoolAsync(School school)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO Schools (Name, StateCode) VALUES (@Name, @StateCode)",
                    conn);
                cmd.Parameters.AddWithValue("Name", school.Name);
                cmd.Parameters.AddWithValue("StateCode", (object)school.StateCode ?? DBNull.Value);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<School> GetSchoolByIdAsync(int schoolId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM Schools WHERE SchoolId = @SchoolId", conn);
                cmd.Parameters.AddWithValue("SchoolId", schoolId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new School
                    {
                        SchoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        StateCode = reader.IsDBNull(reader.GetOrdinal("StateCode"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("StateCode"))
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task UpdateSchoolAsync(School school)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "UPDATE Schools SET Name = @Name, StateCode = @StateCode WHERE SchoolId = @SchoolId",
                    conn);
                cmd.Parameters.AddWithValue("Name", school.Name);
                cmd.Parameters.AddWithValue("StateCode", (object)school.StateCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SchoolId", school.SchoolId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteSchoolAsync(int schoolId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("DELETE FROM Schools WHERE SchoolId = @SchoolId", conn);
                cmd.Parameters.AddWithValue("SchoolId", schoolId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }
    }
}
