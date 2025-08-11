using AutoMapper;
using BBT.MyProjectName.Issues;

namespace BBT.MyProjectName.GitRepositories;

internal class GitRepositoryMapProfile : Profile
{
    public GitRepositoryMapProfile()
    {
        CreateMap<GitRepository, GitRepositoryDto>();
        CreateMap<CreateGitRepositoryInput, GitRepository>();
        CreateMap<UpdateGitRepositoryInput, GitRepository>();
    }
}