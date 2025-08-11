using System;
using BBT.Aether.Domain.Repositories;

namespace BBT.MyProjectName.Issues;

public interface IIssueRepository: IRepository<Issue, Guid>
{
    
}