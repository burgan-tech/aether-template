using System.ComponentModel.DataAnnotations;
using BBT.Aether.Domain.Entities;

namespace BBT.MyProjectName.Issues;

public class AddIssueCommentInput: IHasConcurrencyStamp
{
    [Required]
    [MaxLength(CommentConsts.MaxTextLength)]
    public string Text { get; set; }

    public string ConcurrencyStamp { get; set; }
}