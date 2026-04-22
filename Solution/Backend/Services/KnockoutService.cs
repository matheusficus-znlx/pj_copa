using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public class KnockoutService : IKnockoutService
{
    public KnockoutResult RunKnockout(IReadOnlyList<Group> groups)
    {
        var qualified = groups.ToDictionary(
            group => group.Label,
            group => new { First = group.Teams[0], Second = group.Teams[1] });

        var roundOf16 = PlayStage("Oitavas de Final",
        [
            (qualified["A"].First, qualified["B"].Second),
            (qualified["C"].First, qualified["D"].Second),
            (qualified["E"].First, qualified["F"].Second),
            (qualified["G"].First, qualified["H"].Second),
            (qualified["B"].First, qualified["A"].Second),
            (qualified["D"].First, qualified["C"].Second),
            (qualified["F"].First, qualified["E"].Second),
            (qualified["H"].First, qualified["G"].Second)
        ]);

        var quarterFinals = PlayStage("Quartas de Final", BuildNextRound(roundOf16));
        var semiFinals = PlayStage("Semifinal", BuildNextRound(quarterFinals));
        var final = PlayStage("Final", BuildNextRound(semiFinals));

        return new KnockoutResult(roundOf16, quarterFinals, semiFinals, final[0]);
    }

    public FinalResultRequestDto BuildFinalResult(Match finalMatch)
    {
        return new FinalResultRequestDto
        {
            EquipeA = finalMatch.HomeTeam.Id,
            EquipeB = finalMatch.AwayTeam.Id,
            TeamAGoals = finalMatch.HomeGoals,
            TeamBGoals = finalMatch.AwayGoals,
            TeamAPenaltyGoals = finalMatch.HomePenaltyGoals ?? 0,
            TeamBPenaltyGoals = finalMatch.AwayPenaltyGoals ?? 0
        };
    }

    private static List<Match> PlayStage(string stageName, IReadOnlyList<(Team Home, Team Away)> fixtures)
    {
        return fixtures.Select((fixture, index) => CreateKnockoutMatch(fixture.Home, fixture.Away, $"{stageName} {index + 1}", stageName)).ToList();
    }

    private static List<(Team Home, Team Away)> BuildNextRound(IReadOnlyList<Match> stage)
    {
        var winners = stage.Select(match => match.WinnerId == match.HomeTeam.Id ? match.HomeTeam : match.AwayTeam).ToList();
        return Enumerable.Range(0, winners.Count / 2)
            .Select(index => (winners[index * 2], winners[index * 2 + 1]))
            .ToList();
    }

    private static Match CreateKnockoutMatch(Team homeTeam, Team awayTeam, string stage, string extraLabel)
    {
        var match = new Match
        {
            Stage = stage,
            ExtraLabel = extraLabel,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
            HomeGoals = Random.Shared.Next(0, 4),
            AwayGoals = Random.Shared.Next(0, 4)
        };

        if (match.HomeGoals == match.AwayGoals)
        {
            SimulatePenalties(match);
        }
        else
        {
            match.WinnerId = match.HomeGoals > match.AwayGoals ? homeTeam.Id : awayTeam.Id;
            match.Note = "Vitória no tempo normal";
        }

        return match;
    }

    private static void SimulatePenalties(Match match)
    {
        var home = 0;
        var away = 0;

        for (var round = 0; round < 5; round++)
        {
            home += Random.Shared.NextDouble() > 0.28 ? 1 : 0;
            away += Random.Shared.NextDouble() > 0.28 ? 1 : 0;
        }

        while (home == away)
        {
            home += Random.Shared.NextDouble() > 0.4 ? 1 : 0;
            away += Random.Shared.NextDouble() > 0.4 ? 1 : 0;
        }

        match.HomePenaltyGoals = home;
        match.AwayPenaltyGoals = away;
        match.WinnerId = home > away ? match.HomeTeam.Id : match.AwayTeam.Id;
        match.Note = $"Empate no tempo normal. Pênaltis: {home} x {away}";
    }
}

public record KnockoutResult(
    IReadOnlyList<Match> RoundOf16,
    IReadOnlyList<Match> QuarterFinals,
    IReadOnlyList<Match> SemiFinals,
    Match Final);
