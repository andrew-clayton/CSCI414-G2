// File: Repositories/BookOfferingsRepository.cs

using BookExchangeApi.Models;
using Npgsql;

namespace BookExchangeApi.Repositories
{
    public class BookOfferingsRepository
    {
        private readonly string _connectionString;

        public BookOfferingsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public async Task CreateBookOfferingAsync(BookOffering offering)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO BookOfferings (BookId, AvailabilityStatus, Price, StudentId) VALUES (@BookId, @AvailabilityStatus, @Price, @StudentId)",
                    conn);
                cmd.Parameters.AddWithValue("BookId", offering.BookId);
                cmd.Parameters.AddWithValue("AvailabilityStatus", offering.AvailabilityStatus.ToString());
                cmd.Parameters.AddWithValue("Price", offering.Price);
                cmd.Parameters.AddWithValue("StudentId", offering.StudentId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<BookOffering> GetBookOfferingByIdAsync(int offeringId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM BookOfferings WHERE BookOfferingId = @BookOfferingId", conn);
                cmd.Parameters.AddWithValue("BookOfferingId", offeringId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new BookOffering
                    {
                        BookOfferingId = reader.GetInt32(reader.GetOrdinal("BookOfferingId")),
                        BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                        AvailabilityStatus = Enum.Parse<Availability>(reader.GetString(reader.GetOrdinal("AvailabilityStatus"))),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"))
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

        public async Task UpdateBookOfferingAsync(BookOffering offering)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "UPDATE BookOfferings SET BookId = @BookId, AvailabilityStatus = @AvailabilityStatus, Price = @Price, StudentId = @StudentId WHERE BookOfferingId = @BookOfferingId",
                    conn);
                cmd.Parameters.AddWithValue("BookId", offering.BookId);
                cmd.Parameters.AddWithValue("AvailabilityStatus", offering.AvailabilityStatus.ToString());
                cmd.Parameters.AddWithValue("Price", offering.Price);
                cmd.Parameters.AddWithValue("StudentId", offering.StudentId);
                cmd.Parameters.AddWithValue("BookOfferingId", offering.BookOfferingId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteBookOfferingAsync(int offeringId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("DELETE FROM BookOfferings WHERE BookOfferingId = @BookOfferingId", conn);
                cmd.Parameters.AddWithValue("BookOfferingId", offeringId);
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
