using BBT.Aether.Domain.EntityFrameworkCore;
using BBT.MyProjectName.Issues;
using Microsoft.EntityFrameworkCore;

namespace BBT.MyProjectName.Data;

public class MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options)
    : AetherDbContext<MyProjectNameDbContext>(options)
{
    public virtual DbSet<GitRepository> GitRepositories { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Label> Labels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMyProjectName();
    }
}