namespace Ing_pr_web_api.Domain.Models;

public class Schedule
{
    public int ScheduleId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Teacher Teacher { get; set; }
    public Appointment Appointment { get; set; }
}