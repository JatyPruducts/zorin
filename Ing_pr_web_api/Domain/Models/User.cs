namespace Ing_pr_web_api.Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // Possible values: "user", "teacher", "admin"
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Notification> Notifications { get; set; }
}