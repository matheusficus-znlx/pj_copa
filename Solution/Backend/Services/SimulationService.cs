using Backend.Models;

namespace Backend.Services;

public class SimulationService : ISimulationService
{
    private static readonly (int Home, int Away)[] Fixtures =
    [
        (0, 1), (2, 3), (0, 2), (1, 3), (0, 3), (1, 2)
    ];

    public IReadOnlyList<GroupStageResult> RunGroupStage(IReadOnlyList<Group> groups)
    {
        var result = new List<GroupStageResult>();

        foreach (var group in groups)
        {
            var matches = new List<Match>();

            for (var round = 0; round < Fixtures.Length; round++)
            {
                var (homeIndex, awayIndex) = Fixtures[round];
                var match = CreateMatch(group.Teams[homeIndex], group.Teams[awayIndex], $"Rodada {round + 1}", $"Grupo {group.Label}");
                ApplyStats(match);
                matches.Add(match);
            }

            group.Teams = group.Teams
                .OrderByDescending(team => team.Stats.Points)
                .ThenByDescending(team => team.Stats.GoalDifference)
                .ThenByDescending(team => team.Stats.DrawSeed)
                .ToList();

            result.Add(new GroupStageResult(group.Label, matches));
        }

        return result;
    }

    private static Match CreateMatch(Team homeTeam, Team awayTeam, string stage, string extraLabel)
    {
        var homeGoals = RandomGoals();
        var awayGoals = RandomGoals();

        return new Match
        {
            Stage = stage,
            ExtraLabel = extraLabel,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
            HomeGoals = homeGoals,
            AwayGoals = awayGoals,
            Note = homeGoals == awayGoals ? "Empate no tempo normal" : "Vitória no tempo normal"
        };
    }

    private static void ApplyStats(Match match)
    {
        var home = match.HomeTeam.Stats;
        var away = match.AwayTeam.Stats;

        home.Played++;
        away.Played++;
        home.GoalsFor += match.HomeGoals;
        home.GoalsAgainst += match.AwayGoals;
        away.GoalsFor += match.AwayGoals;
        away.GoalsAgainst += match.HomeGoals;

        if (match.HomeGoals > match.AwayGoals)
        {
            home.Wins++;
            away.Losses++;
            home.Points += 3;
        }
        else if (match.HomeGoals < match.AwayGoals)
        {
            away.Wins++;
            home.Losses++;
            away.Points += 3;
        }
        else
        {
            home.Draws++;
            away.Draws++;
            home.Points++;
            away.Points++;
        }

        home.GoalDifference = home.GoalsFor - home.GoalsAgainst;
        away.GoalDifference = away.GoalsFor - away.GoalsAgainst;
    }

    private static int RandomGoals()
    {
        var bucket = new[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 5 };
        return bucket[Random.Shared.Next(bucket.Length)];
    }
}

public record GroupStageResult(string GroupLabel, IReadOnlyList<Match> Matches);
