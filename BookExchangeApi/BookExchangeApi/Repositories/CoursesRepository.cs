// File: Repositories/CoursesRepository.cs

using BookExchangeApi.Models;
using Npgsql;

namespace BookExchangeApi.Repositories
{
    public class CoursesRepository
    {
        private readonly string _connectionString;

        public CoursesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateCourseAsync(Course course)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO Courses (CourseName, CourseNumber, Professor, FieldOfStudy, SchoolId, BookId) VALUES (@CourseName, @CourseNumber, @Professor, @FieldOfStudy, @SchoolId, @BookId)",
                    conn);
                cmd.Parameters.AddWithValue("CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("CourseNumber", course.CourseNumber);
                cmd.Parameters.AddWithValue("Professor", (object)course.Professor ?? DBNull.Value);
                cmd.Parameters.AddWithValue("FieldOfStudy", (object)course.FieldOfStudy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SchoolId", course.SchoolId);
                cmd.Parameters.AddWithValue("BookId", course.BookId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM Courses WHERE CourseId = @CourseId", conn);
                cmd.Parameters.AddWithValue("CourseId", courseId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Course
                    {
                        CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                        CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                        CourseNumber = reader.GetInt32(reader.GetOrdinal("CourseNumber")),
                        Professor = reader.IsDBNull(reader.GetOrdinal("Professor"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("Professor")),
                        FieldOfStudy = reader.IsDBNull(reader.GetOrdinal("FieldOfStudy"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("FieldOfStudy")),
                        SchoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                        BookId = reader.GetInt32(reader.GetOrdinal("BookId"))
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

        public async Task UpdateCourseAsync(Course course)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "UPDATE Courses SET CourseName = @CourseName, CourseNumber = @CourseNumber, Professor = @Professor, FieldOfStudy = @FieldOfStudy, SchoolId = @SchoolId, BookId = @BookId WHERE CourseId = @CourseId",
                    conn);
                cmd.Parameters.AddWithValue("CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("CourseNumber", course.CourseNumber);
                cmd.Parameters.AddWithValue("Professor", (object)course.Professor ?? DBNull.Value);
                cmd.Parameters.AddWithValue("FieldOfStudy", (object)course.FieldOfStudy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SchoolId", course.SchoolId);
                cmd.Parameters.AddWithValue("BookId", course.BookId);
                cmd.Parameters.AddWithValue("CourseId", course.CourseId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("DELETE FROM Courses WHERE CourseId = @CourseId", conn);
                cmd.Parameters.AddWithValue("CourseId", courseId);
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
