// File: Repositories/BooksRepository.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using BookExchangeApi.Models;

namespace BookExchangeApi.Repositories
{
    public class BooksRepository
    {
        private readonly string _connectionString;

        public BooksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateBookAsync(Book book)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO Books (Title, Description, Author) VALUES (@Title, @Description, @Author)",
                    conn);
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("Description", (object)book.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("Author", (object)book.Author ?? DBNull.Value);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM Books WHERE BookId = @BookId", conn);
                cmd.Parameters.AddWithValue("BookId", bookId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Book
                    {
                        BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Description")),
                        Author = reader.IsDBNull(reader.GetOrdinal("Author"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Author"))
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

        public async Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            try
            {
                var books = new List<Book>();
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM Books WHERE Title ILIKE @Title", conn);
                cmd.Parameters.AddWithValue("Title", $"%{title}%");
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    books.Add(new Book
                    {
                        BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Description")),
                        Author = reader.IsDBNull(reader.GetOrdinal("Author"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Author"))
                    });
                }
                return books;
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "UPDATE Books SET Title = @Title, Description = @Description, Author = @Author WHERE BookId = @BookId",
                    conn);
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("Description", (object)book.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("Author", (object)book.Author ?? DBNull.Value);
                cmd.Parameters.AddWithValue("BookId", book.BookId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("DELETE FROM Books WHERE BookId = @BookId", conn);
                cmd.Parameters.AddWithValue("BookId", bookId);
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
