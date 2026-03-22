using BBT.Aether.Mapper.Mapperly;
using Riok.Mapperly.Abstractions;

namespace BBT.MyProjectName.Issues;

[Mapper]
public partial class IssueMapper: MapperBase<Issue, IssueDto>
{
    public override partial IssueDto Map(Issue source);

    public override partial IssueDto Map(Issue source, IssueDto destination);
}

[Mapper]
public partial class IssueListMapper: MapperBase<List<Issue>, List<IssueDto>>
{
    public override partial List<IssueDto> Map(List<Issue> source);

    public override partial List<IssueDto> Map(List<Issue> source, List<IssueDto> destination);
}
