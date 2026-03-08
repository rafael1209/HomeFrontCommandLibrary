namespace HomeFrontCommandLibrary.Models;

public class City
{
    public int Id { get; set; }
    public int AreaId { get; set; }
    public required CityName Name { get; set; }
    public required ReshutName Reshut { get; set; }
    public string AreaName { get; set; } = string.Empty;
    public int ProtectionTime { get; set; }
}

public class CityName
{
    public string Hebrew { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Russian { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}

public class ReshutName
{
    public string Hebrew { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Russian { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}