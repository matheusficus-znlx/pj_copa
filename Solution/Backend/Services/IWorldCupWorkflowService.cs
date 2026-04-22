using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public interface IWorldCupWorkflowService
{
    Task<IReadOnlyList<Team>> GetTeamsAsync(string gitUser, CancellationToken cancellationToken);
    Task<SimulationResponseDto> SimulateAsync(string gitUser, SimulationRequestDto request, CancellationToken cancellationToken);
}
