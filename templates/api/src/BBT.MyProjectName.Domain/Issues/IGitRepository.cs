using System;
using BBT.Aether.Domain.Repositories;

namespace BBT.MyProjectName.Issues;

public interface IGitRepository : IRepository<GitRepository, Guid>
{
    
}