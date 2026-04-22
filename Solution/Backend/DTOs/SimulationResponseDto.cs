using Backend.Models;
using Backend.Services;

namespace Backend.DTOs;

public record SimulationResponseDto(
    IReadOnlyList<Team> Teams,
    IReadOnlyList<Group> Groups,
    IReadOnlyList<GroupStageResult> GroupStage,
    KnockoutResult Knockout,
    FinalResultRequestDto FinalResult,
    bool FinalResultSent);
