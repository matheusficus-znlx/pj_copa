using Backend.Models;

namespace Backend.Services;

public interface ICupService
{
    IReadOnlyList<Group> DrawGroups(IReadOnlyList<Team> teams);
}
