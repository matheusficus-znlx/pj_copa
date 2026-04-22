using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public interface IKatalystApiService
{
    Task<IReadOnlyList<Team>> GetAllTeamsAsync(string gitUser, CancellationToken cancellationToken);
    Task SendFinalResultAsync(string gitUser, FinalResultRequestDto payload, CancellationToken cancellationToken);
}
