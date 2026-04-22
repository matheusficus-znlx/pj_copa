namespace Backend.DTOs;

public class FinalResultRequestDto
{
    public string EquipeA { get; set; } = string.Empty;
    public string EquipeB { get; set; } = string.Empty;
    public int TeamAGoals { get; set; }
    public int TeamBGoals { get; set; }
    public int TeamAPenaltyGoals { get; set; }
    public int TeamBPenaltyGoals { get; set; }
}
