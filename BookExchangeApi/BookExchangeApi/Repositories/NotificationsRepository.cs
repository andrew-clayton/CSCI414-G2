// File: Repositories/NotificationsRepository.cs

using BookExchangeApi.Models;
using Npgsql;

namespace BookExchangeApi.Repositories
{
    public class NotificationsRepository
    {
        private readonly string _connectionString;

        public NotificationsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "INSERT INTO Notifications (StudentId, BookOfferingId, NotificationType, Timestamp) VALUES (@StudentId, @BookOfferingId, @NotificationType, @Timestamp)",
                    conn);
                cmd.Parameters.AddWithValue("StudentId", notification.StudentId);
                cmd.Parameters.AddWithValue("BookOfferingId", notification.BookOfferingId);
                cmd.Parameters.AddWithValue("NotificationType", notification.NotificationType.ToString());
                cmd.Parameters.AddWithValue("Timestamp", notification.Timestamp);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task<Notification> GetNotificationByIdAsync(int notificationId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("SELECT * FROM Notifications WHERE NotificationId = @NotificationId", conn);
                cmd.Parameters.AddWithValue("NotificationId", notificationId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Notification
                    {
                        NotificationId = reader.GetInt32(reader.GetOrdinal("NotificationId")),
                        StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                        BookOfferingId = reader.GetInt32(reader.GetOrdinal("BookOfferingId")),
                        NotificationType = Enum.Parse<NotificationType>(reader.GetString(reader.GetOrdinal("NotificationType"))),
                        Timestamp = reader.GetFieldValue<DateTimeOffset>(reader.GetOrdinal("Timestamp"))
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

        public async Task UpdateNotificationAsync(Notification notification)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(
                    "UPDATE Notifications SET StudentId = @StudentId, BookOfferingId = @BookOfferingId, NotificationType = @NotificationType, Timestamp = @Timestamp WHERE NotificationId = @NotificationId",
                    conn);
                cmd.Parameters.AddWithValue("StudentId", notification.StudentId);
                cmd.Parameters.AddWithValue("BookOfferingId", notification.BookOfferingId);
                cmd.Parameters.AddWithValue("NotificationType", notification.NotificationType.ToString());
                cmd.Parameters.AddWithValue("Timestamp", notification.Timestamp);
                cmd.Parameters.AddWithValue("NotificationId", notification.NotificationId);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception (ex)
                throw;
            }
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand("DELETE FROM Notifications WHERE NotificationId = @NotificationId", conn);
                cmd.Parameters.AddWithValue("NotificationId", notificationId);
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
