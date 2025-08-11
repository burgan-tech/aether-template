using BBT.Aether.Application.Dtos;
using BBT.Aether.Domain.Entities;

namespace BBT.MyProjectName.GitRepositories;

public class GitRepositoryDto: EntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; }
    public string ConcurrencyStamp { get; set; }
}