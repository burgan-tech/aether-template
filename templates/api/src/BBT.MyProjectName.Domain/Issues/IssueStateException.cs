using BBT.Aether;

namespace BBT.MyProjectName.Issues;

public class IssueStateException(string message) : BusinessException(
    MyProjectNameErrorCodes.IssueState,
    message
);