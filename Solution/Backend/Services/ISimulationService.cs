using Backend.Models;

namespace Backend.Services;

public interface ISimulationService
{
    IReadOnlyList<GroupStageResult> RunGroupStage(IReadOnlyList<Group> groups);
}
