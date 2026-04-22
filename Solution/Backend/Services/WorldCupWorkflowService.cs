using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public class WorldCupWorkflowService : IWorldCupWorkflowService
{
    private readonly IKatalystApiService _apiService;
    private readonly ICupService _cupService;
    private readonly ISimulationService _simulationService;
    private readonly IKnockoutService _knockoutService;

    public WorldCupWorkflowService(
        IKatalystApiService apiService,
        ICupService cupService,
        ISimulationService simulationService,
        IKnockoutService knockoutService)
    {
        _apiService = apiService;
        _cupService = cupService;
        _simulationService = simulationService;
        _knockoutService = knockoutService;
    }

    public Task<IReadOnlyList<Team>> GetTeamsAsync(string gitUser, CancellationToken cancellationToken)
    {
        return _apiService.GetAllTeamsAsync(gitUser, cancellationToken);
    }

    public async Task<SimulationResponseDto> SimulateAsync(string gitUser, SimulationRequestDto request, CancellationToken cancellationToken)
    {
        var teams = await _apiService.GetAllTeamsAsync(gitUser, cancellationToken);
        var groups = _cupService.DrawGroups(teams);
        var groupStage = _simulationService.RunGroupStage(groups);
        var knockout = _knockoutService.RunKnockout(groups);
        var finalPayload = _knockoutService.BuildFinalResult(knockout.Final);

        if (request.SendFinalResult)
        {
            await _apiService.SendFinalResultAsync(gitUser, finalPayload, cancellationToken);
        }

        return new SimulationResponseDto(
            teams,
            groups,
            groupStage,
            knockout,
            finalPayload,
            request.SendFinalResult);
    }
}
