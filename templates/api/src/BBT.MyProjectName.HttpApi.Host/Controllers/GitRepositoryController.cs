using BBT.MyProjectName.GitRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BBT.MyProjectName.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/repositories")]
public sealed class GitRepositoryController(IGitRepositoryAppService appService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var item = await appService.GetAsync(id);
        return Ok(item);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedListAsync([FromQuery] GetGitRepositoriesInput input)
    {
        var item = await appService.GetListAsync(input);
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateGitRepositoryInput input)
    {
        var item = await appService.CreateAsync(input);
        return Ok(item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateGitRepositoryInput input)
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
}