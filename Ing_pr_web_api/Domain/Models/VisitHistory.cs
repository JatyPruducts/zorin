namespace Ing_pr_web_api.Domain.Models;

public class VisitHistory
{
    public int HistoryId { get; set; }
    public DateTime VisitDate { get; set; }
    public string Feedback { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Appointment Appointment { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}