namespace Ing_pr_web_api.Domain.Models;

public class Analytics
{
    public int AnalyticsId { get; set; }
    public string ProgressReport { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Teacher Teacher { get; set; }
    public User User { get; set; }
    public Appointment Appointment { get; set; }
}