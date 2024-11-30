namespace Ing_pr_web_api.Domain.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } // Possible values: "scheduled", "completed", "cancelled"
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int SpecialistId { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Specialist Specialist { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public VisitHistory VisitHistory { get; set; }
}