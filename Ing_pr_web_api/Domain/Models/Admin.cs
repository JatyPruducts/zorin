namespace Ing_pr_web_api.Domain.Models;

public class Admin
{
    public int AdminId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; } // Linked to the User table
}