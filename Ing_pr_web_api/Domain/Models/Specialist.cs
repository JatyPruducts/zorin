namespace Ing_pr_web_api.Domain.Models;

public class Specialist
{
    public int SpecialistId { get; set; }
    public string Specialization { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}