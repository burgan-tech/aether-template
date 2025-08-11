using System.ComponentModel.DataAnnotations;
using BBT.MyProjectName.Issues;

namespace BBT.MyProjectName.GitRepositories;

public class CreateGitRepositoryInput
{
    [Required]
    [StringLength(GitRepositoryConsts.MaxNameLength)]
    public string Name { get; set; }
}