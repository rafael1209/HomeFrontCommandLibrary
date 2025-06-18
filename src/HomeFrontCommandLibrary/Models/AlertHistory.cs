namespace HomeFrontCommandLibrary.Models;

public class AlertHistory
{
    public Category Category { get; set; } = new();
    public City City { get; set; } = new();
    public DateTime AlertDate { get; set; }
}