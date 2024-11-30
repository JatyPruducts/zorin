namespace Ing_pr_web_api.Domain.Models;

public class Notification
{
    public int NotificationId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Appointment Appointment { get; set; }
}