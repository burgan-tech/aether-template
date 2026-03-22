using BBT.Aether.Mapper.Mapperly;
using BBT.MyProjectName.Issues;
using Riok.Mapperly.Abstractions;

namespace BBT.MyProjectName.GitRepositories;

[Mapper]
public partial class GitRepositoryMapper: MapperBase<GitRepository, GitRepositoryDto>
{
    public override partial GitRepositoryDto Map(GitRepository source);
    public override partial GitRepositoryDto Map(GitRepository source, GitRepositoryDto destination);
}

[Mapper]
public partial class GitRepositoryCreateInputMapper: MapperBase<CreateGitRepositoryInput, GitRepository>
{
    public override partial GitRepository Map(CreateGitRepositoryInput source);
    public override partial GitRepository Map(CreateGitRepositoryInput source, GitRepository destination);
}

[Mapper]
public partial class GitRepositoryUpdateInputMapper: MapperBase<UpdateGitRepositoryInput, GitRepository>
{
    public override partial GitRepository Map(UpdateGitRepositoryInput source);
    public override partial GitRepository Map(UpdateGitRepositoryInput source, GitRepository destination);
}
