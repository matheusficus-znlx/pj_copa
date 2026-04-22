using Backend.Models;

namespace Backend.Services;

public class CupService : ICupService
{
    public IReadOnlyList<Group> DrawGroups(IReadOnlyList<Team> teams)
    {
        var shuffled = teams.OrderBy(_ => Random.Shared.Next()).ToList();
        return "ABCDEFGH".Select((label, index) => new Group
        {
            Label = label.ToString(),
            Teams = shuffled.Skip(index * 4).Take(4).Select(CloneTeam).ToList()
        }).ToList();
    }

    private static Team CloneTeam(Team team)
    {
        return new Team
        {
            Id = team.Id,
            Name = team.Name,
            Stats = new TeamStats { DrawSeed = Random.Shared.NextDouble() }
        };
    }
}
