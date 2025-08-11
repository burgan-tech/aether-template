using BBT.Aether.Testing;

namespace BBT.MyProjectName;

public abstract class DomainTestBase<TEntry> : MyProjectNameTestBase<TEntry>
    where TEntry : ModuleEntryPointBase, new();