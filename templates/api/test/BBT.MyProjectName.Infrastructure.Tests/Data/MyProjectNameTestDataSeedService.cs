using System;
using System.Threading.Tasks;
using BBT.Aether.Domain.Services;
using BBT.MyProjectName.Issues;

namespace BBT.MyProjectName.Data;

/// <summary>
/// This service is responsible for seeding test data into the repositories.
/// It implements the <see cref="IDataSeedService"/> interface.
/// </summary>
public class MyProjectNameTestDataSeedService(
    TestData testData,
    IGitRepository gitRepository,
    IIssueRepository issueRepository) : IDataSeedService
{
    /// <summary>
    /// Seeds the data asynchronously.
    /// </summary>
    /// <param name="context">The seed context.</param>
    public async Task SeedAsync(SeedContext context)
    {
        await SeedRepository();
        await SeedIssueRepository();
    }

    /// <summary>
    /// Seeds the Git repository with test data.
    /// </summary>
    private async Task SeedRepository()
    {
        var gitRepository_1 = new GitRepository(
            testData.Repository_Id_1,
            testData.Repository_Name_1);

        var gitRepository_2 = new GitRepository(
            testData.Repository_Id_2,
            testData.Repository_Name_2);

        await gitRepository.InsertAsync(gitRepository_1, true);
        await gitRepository.InsertAsync(gitRepository_2, true);
    }

    /// <summary>
    /// Seeds the issue repository with test data.
    /// </summary>
    private async Task SeedIssueRepository()
    {
        var issue_1 = new Issue(
            testData.Issue_Id_1,
            testData.Repository_Id_1,
            testData.Issue_Title_1,
            "Test issue 1 description");

        var issue_2 = new Issue(
            testData.Issue_Id_2,
            testData.Repository_Id_2,
            testData.Issue_Title_2,
            "Test issue 2 description");

        var issue_3 = new Issue(
            testData.Issue_Id_3,
            testData.Repository_Id_2,
            testData.Issue_Title_3,
            "Test issue 3 description");

        await issueRepository.InsertAsync(issue_1, true);
        await issueRepository.InsertAsync(issue_2, true);
        await issueRepository.InsertAsync(issue_3, true);
    }
}