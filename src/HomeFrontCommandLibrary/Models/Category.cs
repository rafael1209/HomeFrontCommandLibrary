namespace HomeFrontCommandLibrary.Models;

public class Category
{
    public int Id { get; set; }
    public int MatrixId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}