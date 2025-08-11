using BBT.Aether.Application.Services;
using BBT.Aether.Domain.Repositories;
using BBT.MyProjectName.Issues;

namespace BBT.MyProjectName.GitRepositories;

public sealed class GitRepositoryAppService(IServiceProvider serviceProvider, IGitRepository repository) : CrudAppService<
    GitRepository, 
    GitRepositoryDto, 
    Guid, 
    GetGitRepositoriesInput, 
    CreateGitRepositoryInput, 
    UpdateGitRepositoryInput>(serviceProvider, repository), IGitRepositoryAppService
{
    
}