using BBT.Aether.Application;

namespace BBT.MyProjectName.GitRepositories;

public interface IGitRepositoryAppService : ICrudAppService<GitRepositoryDto, Guid, GetGitRepositoriesInput,
    CreateGitRepositoryInput, UpdateGitRepositoryInput>
{
}