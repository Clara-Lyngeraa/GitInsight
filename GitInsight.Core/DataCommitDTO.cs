namespace GitInsight.Core;

public record CommitDTO(string name, string email, DateTimeOffset date);
public record CommitUpdateDTO(string repoId, string name, string email, DateTimeOffset date);
public record CommitCreateDTO(string name, string email, DateTimeOffset date);