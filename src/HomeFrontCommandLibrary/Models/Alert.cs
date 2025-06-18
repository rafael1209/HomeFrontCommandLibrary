namespace HomeFrontCommandLibrary.Models;

public class Alert
{
    public Category? Category { get; set; }
    public List<City>? Cities { get; set; }
    public DateTime AlertDate { get; set; }
}