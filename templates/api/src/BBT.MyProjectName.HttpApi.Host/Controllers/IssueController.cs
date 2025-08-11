using BBT.MyProjectName.Issues;
using Microsoft.AspNetCore.Mvc;

namespace BBT.MyProjectName.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/issues")]
public sealed class IssueController(IIssueAppService appService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPagedListAsync([FromQuery] GetIssuesInput input)
    {
        var item = await appService.GetListAsync(input);
        return Ok(item);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var item = await appService.GetAsync(id);
        return Ok(item);
    }

    [HttpPost("{repositoryId}")]
    public async Task<IActionResult> CreateAsync(Guid repositoryId, CreateIssueInput input)
    {
        var item = await appService.CreateAsync(repositoryId, input);
        return Ok(item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateIssueInput input)
    {
        var item = await appService.UpdateAsync(id, input);
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await appService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}/close")]
    public async Task<IActionResult> CloseAsync(Guid id, CloseIssueInput input)
    {
        await appService.CloseAsync(id, input);
        return Ok();
    }

    [HttpPut("{id}/reopen")]
    public async Task<IActionResult> ReOpenAsync(Guid id)
    {
        await appService.ReOpenAsync(id);
        return Ok();
    }

    [HttpPost("{id}/comment")]
    public async Task<IActionResult> AddCommentAsync(Guid id, AddIssueCommentInput input)
    {
        await appService.AddCommentAsync(id, input);
        return Ok();
    }
}