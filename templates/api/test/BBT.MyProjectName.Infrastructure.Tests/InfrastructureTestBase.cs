using BBT.Aether.Testing;

namespace BBT.MyProjectName;

public class InfrastructureTestBase<TEntry> : MyProjectNameTestBase<TEntry>
    where TEntry : ModuleEntryPointBase, new();