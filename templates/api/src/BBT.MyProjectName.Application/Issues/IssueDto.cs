using BBT.Aether.Application.Dtos;
using BBT.Aether.Domain.Entities;

namespace BBT.MyProjectName.Issues;

public class IssueDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid RepositoryId { get; set; }
    public string Title { get; set; }
    public string? Text { get; set; }
    public bool IsLocked { get; set; }
    public bool IsClosed { get; set; }
    public IssueCloseReason? CloseReason { get; set; }
    public Guid? AssignedUserId { get; set; }
    public DateTime? LastCommentTime { get; set; }
    public string ConcurrencyStamp { get; set; }
    public string[]? Tags { get; set; }
}