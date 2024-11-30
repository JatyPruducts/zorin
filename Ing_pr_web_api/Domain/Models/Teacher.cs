namespace Ing_pr_web_api.Domain.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string Specialization { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; } // Linked to the User table
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Analytics> AnalyticsReports { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
}