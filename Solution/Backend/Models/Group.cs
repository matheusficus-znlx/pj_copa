namespace Backend.Models;

public class Group
{
    public string Label { get; set; } = string.Empty;
    public List<Team> Teams { get; set; } = [];
}
