using BBT.Aether;
using BBT.Aether.Domain.Entities;

namespace BBT.MyProjectName.Issues;

public class GitRepository : AggregateRoot<Guid>
{
    public string Name { get; set; }

    public GitRepository(
        Guid id,
        string name) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
    }

    private GitRepository()
    {
        //For orm
    }
}