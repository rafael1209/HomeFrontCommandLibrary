namespace HomeFrontCommandLibrary.Models;

public class AlertHistory
{
    public required Category Category { get; set; }
    public required City City { get; set; }
    public DateTime AlertDate { get; set; }
}