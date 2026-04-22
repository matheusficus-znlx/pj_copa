using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorldCupController : ControllerBase
{
    private readonly IWorldCupWorkflowService _workflowService;

    public WorldCupController(IWorldCupWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    [HttpGet("teams")]
    public async Task<IActionResult> GetTeams()
    {
        var gitUser = GetGitUser();
        if (string.IsNullOrWhiteSpace(gitUser))
        {
            return BadRequest("Header git-user is required.");
        }

        var teams = await _workflowService.GetTeamsAsync(gitUser, HttpContext.RequestAborted);
        return Ok(teams);
    }

    [HttpPost("simulate")]
    public async Task<IActionResult> Simulate([FromBody] SimulationRequestDto? request)
    {
        var gitUser = GetGitUser();
        if (string.IsNullOrWhiteSpace(gitUser))
        {
            return BadRequest("Header git-user is required.");
        }

        var response = await _workflowService.SimulateAsync(
            gitUser,
            request ?? new SimulationRequestDto(),
            HttpContext.RequestAborted);

        return Ok(response);
    }

    private string? GetGitUser()
    {
        return Request.Headers.TryGetValue("git-user", out var gitUser)
            ? gitUser.ToString()
            : null;
    }
}
