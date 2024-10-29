// File: Repositories/StudentsRepository.cs

using BookExchangeApi.Models;
using Npgsql;

namespace BookExchangeApi.Repositories
{
    public class StudentsRepository
    {
        private readonly string _connectionString;

        public StudentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateStudentAsync(Student student)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO Students (FirstName, LastName, SchoolId, FieldOfStudy) VALUES (@FirstName, @LastName, @SchoolId, @FieldOfStudy)",
                    conn);
                cmd.Parameters.AddWithValue("FirstName", (object)student.FirstName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("LastName", (object)student.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SchoolId", student.SchoolId);
                cmd.Parameters.AddWithValue("FieldOfStudy", (object)student.FieldOfStudy ?? DBNull.Value);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM Students WHERE StudentId = @StudentId", conn);
                cmd.Parameters.AddWithValue("StudentId", studentId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Student
                    {
                        StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                        FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.IsDBNull(reader.GetOrdinal("LastName"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("LastName")),
                        SchoolId = reader.GetInt32(reader.GetOrdinal("SchoolId")),
                        FieldOfStudy = reader.IsDBNull(reader.GetOrdinal("FieldOfStudy"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("FieldOfStudy"))
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

        public async Task UpdateStudentAsync(Student student)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, SchoolId = @SchoolId, FieldOfStudy = @FieldOfStudy WHERE StudentId = @StudentId",
                    conn);
                cmd.Parameters.AddWithValue("FirstName", (object)student.FirstName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("LastName", (object)student.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("SchoolId", student.SchoolId);
                cmd.Parameters.AddWithValue("FieldOfStudy", (object)student.FieldOfStudy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("StudentId", student.StudentId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("DELETE FROM Students WHERE StudentId = @StudentId", conn);
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
