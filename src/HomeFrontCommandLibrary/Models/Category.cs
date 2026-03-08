namespace HomeFrontCommandLibrary.Models;

public class Category
{
    public int Id { get; set; }
    public int MatrixId { get; set; }
    public required CategoryTitle Title { get; set; }
    public required CategoryDescription Description { get; set; }
}

public class CategoryTitle
{
    public string Hebrew { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Russian { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}

public class CategoryDescription
{
    public string Hebrew { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Russian { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}