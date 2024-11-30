using Ing_pr_web_api.Application.Interfaces;
using Ing_pr_web_api.Domain.Models;
using Npgsql;

namespace Ing_pr_web_api.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly string _connectionString;

        public AppointmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            var appointments = new List<Appointment>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM appointments", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var appointment = new Appointment
                        {
                            AppointmentId = reader.GetInt32(reader.GetOrdinal("appointment_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            SpecialistId = reader.GetInt32(reader.GetOrdinal("specialist_id")),
                            AppointmentDate = reader.GetDateTime(reader.GetOrdinal("appointment_date")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                        };
                        appointments.Add(appointment);
                    }
                }
            }

            return appointments;
        }

        public async Task<Appointment> GetByIdAsync(int appointmentId)
        {
            Appointment appointment = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("SELECT * FROM appointments WHERE appointment_id = @AppointmentId", connection);
                command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        appointment = new Appointment
                        {
                            AppointmentId = reader.GetInt32(reader.GetOrdinal("appointment_id")),
                            UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                            SpecialistId = reader.GetInt32(reader.GetOrdinal("specialist_id")),
                            AppointmentDate = reader.GetDateTime(reader.GetOrdinal("appointment_date")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
                        };
                    }
                }
            }

            return appointment;
        }

        public async Task AddAsync(Appointment appointment)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand(@"
                    INSERT INTO appointments (user_id, specialist_id, appointment_date, status, created_at)
                    VALUES (@UserId, @SpecialistId, @AppointmentDate, @Status, @CreatedAt)", connection);

                command.Parameters.AddWithValue("@UserId", appointment.User.UserId);
                command.Parameters.AddWithValue("@SpecialistId", appointment.Specialist.SpecialistId);
                command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@Status", appointment.Status);
                command.Parameters.AddWithValue("@CreatedAt", appointment.CreatedAt);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand(@"
                    UPDATE appointments
                    SET user_id = @UserId,
                        specialist_id = @SpecialistId,
                        appointment_date = @AppointmentDate,
                        status = @Status,
                        created_at = @CreatedAt
                    WHERE appointment_id = @AppointmentId", connection);

                command.Parameters.AddWithValue("@UserId", appointment.User.UserId);
                command.Parameters.AddWithValue("@SpecialistId", appointment.Specialist.SpecialistId);
                command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@Status", appointment.Status);
                command.Parameters.AddWithValue("@CreatedAt", appointment.CreatedAt);
                command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int appointmentId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var command = new NpgsqlCommand("DELETE FROM appointments WHERE appointment_id = @AppointmentId", connection);
                command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
}