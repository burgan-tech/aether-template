using BBT.Aether.Testing;

namespace BBT.MyProjectName.Issues;

public abstract class IssueRepositoryTests<TEntry> : DomainTestBase<TEntry>
    where TEntry : ModuleEntryPointBase, new()
{
}