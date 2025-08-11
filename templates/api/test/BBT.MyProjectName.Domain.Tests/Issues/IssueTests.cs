using System;
using System.Linq;
using Xunit;

namespace BBT.MyProjectName.Issues;

public class IssueTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var repositoryId = Guid.NewGuid();
        var title = "Test Issue";
        var text = "Issue text";
        var assignedUserId = Guid.NewGuid();

        // Act
        var issue = new Issue(id, repositoryId, title, text, assignedUserId);

        // Assert
        Assert.Equal(id, issue.Id);
        Assert.Equal(repositoryId, issue.RepositoryId);
        Assert.Equal(title, issue.Title);
        Assert.Equal(text, issue.Text);
        Assert.Equal(assignedUserId, issue.AssignedUserId);
        Assert.NotNull(issue.Labels);
        Assert.NotNull(issue.Comments);
        Assert.Empty(issue.Labels);
        Assert.Empty(issue.Comments);
    }

    [Fact]
    public void SetTitle_WithValidTitle_ShouldUpdateTitle()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Initial Title");
        var newTitle = "Updated Title";

        // Act
        issue.SetTitle(newTitle);

        // Assert
        Assert.Equal(newTitle, issue.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetTitle_WithNullOrWhitespace_ShouldThrowException(string invalidTitle)
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Initial Title");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => issue.SetTitle(invalidTitle));
    }

    [Fact]
    public void Close_ShouldMarkIssueAsClosedAndSetCloseReason()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");
        var closeReason = IssueCloseReason.Resolved;

        // Act
        issue.Close(closeReason);

        // Assert
        Assert.True(issue.IsClosed);
        Assert.Equal(closeReason, issue.CloseReason);
    }

    [Fact]
    public void ReOpen_WhenNotLocked_ShouldReopenIssue()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");
        issue.Close(IssueCloseReason.Resolved);

        // Act
        issue.ReOpen();

        // Assert
        Assert.False(issue.IsClosed);
        Assert.Null(issue.CloseReason);
    }

    [Fact]
    public void ReOpen_WhenLocked_ShouldThrowIssueStateException()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");
        issue.Close(IssueCloseReason.Resolved);
        issue.Lock();

        // Act & Assert
        Assert.Throws<IssueStateException>(() => issue.ReOpen());
    }

    [Fact]
    public void Lock_WhenIssueIsClosed_ShouldLockIssue()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");
        issue.Close(IssueCloseReason.Resolved);

        // Act
        issue.Lock();

        // Assert
        Assert.True(issue.IsLocked);
    }

    [Fact]
    public void Lock_WhenIssueIsNotClosed_ShouldThrowIssueStateException()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");

        // Act & Assert
        Assert.Throws<IssueStateException>(() => issue.Lock());
    }

    [Fact]
    public void Unlock_ShouldSetIsLockedToFalse()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");
        issue.Close(IssueCloseReason.Resolved);
        issue.Lock();

        // Act
        issue.Unlock();

        // Assert
        Assert.False(issue.IsLocked);
    }

    [Fact]
    public void AddComment_ShouldAddNewCommentToCommentsCollection()
    {
        // Arrange
        var issue = new Issue(Guid.NewGuid(), Guid.NewGuid(), "Title");
        var commentText = "This is a comment.";
        var userId = Guid.NewGuid();
        var initialCount = issue.Comments.Count;

        // Act
        issue.AddComment(commentText, userId);

        // Assert
        Assert.Equal(initialCount + 1, issue.Comments.Count);
        var addedComment = issue.Comments.Single();
        Assert.Equal(commentText, addedComment.Text);
        Assert.Equal(userId, addedComment.UserId);
        Assert.Equal(issue.Id, addedComment.IssueId);
    }
}