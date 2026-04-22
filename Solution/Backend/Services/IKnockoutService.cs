using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public interface IKnockoutService
{
    KnockoutResult RunKnockout(IReadOnlyList<Group> groups);
    FinalResultRequestDto BuildFinalResult(Match finalMatch);
}
