namespace GitInsight.Core;

public record CommitDTO(string StringId, string Name, DateTime Date);
public record CommitCreateDTO(string StringId, string Name, DateTime Date);
public record CommitUpdateDTO(string StringId, string Name, DateTime Date);