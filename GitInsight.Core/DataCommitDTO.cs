namespace GitInsight.Core;

public record CommitDTO(string StringId, string RepoId, string Name, string Email, DateTimeOffset Date);
public record CommitCreateDTO(string StringId, string Name, string RepoId, string Email, DateTimeOffset Date);
public record CommitUpdateDTO(string RepoId, string Name, string Email, DateTimeOffset Date);