namespace GitInsight.Entities.DTOs;

public record DataCommitDTO(string StringId, string Name, DateTime Date);
public record DataCommitCreateDTO(string StringId, string Name, DateTime Date);
public record DataCommitUpdateDTO(string StringId, string Name, DateTime Date);