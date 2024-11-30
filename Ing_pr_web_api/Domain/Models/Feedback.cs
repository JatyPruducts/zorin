namespace Ing_pr_web_api.Domain.Models;

public class Feedback
{
    public int FeedbackId { get; set; }
    public int Rating { get; set; } // 1 to 5
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public VisitHistory VisitHistory { get; set; }
}