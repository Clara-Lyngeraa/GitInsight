namespace GitInsight.Core;

public record CommitSignatureUpdateDTO(int repoId, string name, string email, DateTimeOffset date);