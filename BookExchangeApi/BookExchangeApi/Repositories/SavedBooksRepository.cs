// File: Repositories/SavedBooksRepository.cs

using BookExchangeApi.Models;
using Npgsql;

namespace BookExchangeApi.Repositories
{
    public class SavedBooksRepository
    {
        private readonly string _connectionString;

        public SavedBooksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public async Task CreateSavedBookAsync(SavedBook savedBook)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO SavedBooks (BookId, StudentId) VALUES (@BookId, @StudentId)",
                    conn);
                cmd.Parameters.AddWithValue("BookId", savedBook.BookId);
                cmd.Parameters.AddWithValue("StudentId", savedBook.StudentId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<List<SavedBook>> GetSavedBooksByStudentIdAsync(int studentId)
        {
            try
            {
                var savedBooks = new List<SavedBook>();
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM SavedBooks WHERE StudentId = @StudentId", conn);
                cmd.Parameters.AddWithValue("StudentId", studentId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    savedBooks.Add(new SavedBook
                    {
                        BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                        StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"))
                    });
                }
                return savedBooks;
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteSavedBookAsync(int bookId, int studentId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "DELETE FROM SavedBooks WHERE BookId = @BookId AND StudentId = @StudentId",
                    conn);
                cmd.Parameters.AddWithValue("BookId", bookId);
                cmd.Parameters.AddWithValue("StudentId", studentId);
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
