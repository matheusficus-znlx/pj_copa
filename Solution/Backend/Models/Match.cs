namespace Backend.Models;

public class Match
{
    public string Stage { get; set; } = string.Empty;
    public string ExtraLabel { get; set; } = string.Empty;
    public Team HomeTeam { get; set; } = new();
    public Team AwayTeam { get; set; } = new();
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }
    public int? HomePenaltyGoals { get; set; }
    public int? AwayPenaltyGoals { get; set; }
    public string WinnerId { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}
