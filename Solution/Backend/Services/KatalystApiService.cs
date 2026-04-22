using System.Net.Http.Json;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public class KatalystApiService : IKatalystApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public KatalystApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IReadOnlyList<Team>> GetAllTeamsAsync(string gitUser, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient("Katalyst");
        client.DefaultRequestHeaders.Remove("git-user");
        client.DefaultRequestHeaders.Add("git-user", gitUser);

        var response = await client.GetFromJsonAsync<List<KatalystTeamResponse>>("GetAllTeams", cancellationToken);
        return response?.Select(team => new Team
        {
            Id = team.Token,
            Name = team.Nome,
            Stats = new TeamStats { DrawSeed = Random.Shared.NextDouble() }
        }).ToList() ?? [];
    }

    public async Task SendFinalResultAsync(string gitUser, FinalResultRequestDto payload, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient("Katalyst");
        client.DefaultRequestHeaders.Remove("git-user");
        client.DefaultRequestHeaders.Add("git-user", gitUser);

        var response = await client.PostAsJsonAsync("FinalResult", payload, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private sealed class KatalystTeamResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
