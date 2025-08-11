using System;
using System.Threading.Tasks;
using BBT.Aether.Domain.Entities;
using BBT.Aether.Testing;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace BBT.MyProjectName.Issues;

public abstract class IssueAppServiceTests<TEntry> : ApplicationTestBase<TEntry>
    where TEntry : ModuleEntryPointBase, new()
{
    private readonly IIssueAppService _issueAppService;
    private readonly TestData _testData;

    protected IssueAppServiceTests()
    {
        _issueAppService = GetRequiredService<IIssueAppService>();
        _testData = GetRequiredService<TestData>();
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnPagedResult()
    {
        // Arrange
        var pagedInput = new GetIssuesInput() { SkipCount = 0, MaxResultCount = 10 };

        // Act
        var result = await _issueAppService.GetListAsync(pagedInput);

        // Assert
        result.ShouldNotBeNull();
        result.Items.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task CreateAsync_NewIssue_ShouldBeCreated()
    {
        // Arrange
        var issueInput = new CreateIssueInput() { Title = "Test Issue", Text = "Issue text", Tags = ["test"] };

        // Act
        var result = await _issueAppService.CreateAsync(_testData.Repository_Id_1, issueInput);

        // Assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetAsync_ThrowNotFound_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _issueAppService.GetAsync(id);
        });
    }
}