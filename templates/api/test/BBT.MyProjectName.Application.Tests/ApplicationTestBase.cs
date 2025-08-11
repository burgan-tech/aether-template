using BBT.Aether.Testing;

namespace BBT.MyProjectName;

public class ApplicationTestBase<TEntry> : MyProjectNameTestBase<TEntry>
    where TEntry : ModuleEntryPointBase, new();