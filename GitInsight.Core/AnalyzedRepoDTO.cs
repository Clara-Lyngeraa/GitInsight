namespace GitInsight.Core;

public record AnalyzedRepoDTO(int Id, string RepositoryIdString, DateTime State, ICollection<int> commitsInRepo);

public record AnalyzedRepoCreateDTO(string RepositoryIdString, DateTime State, ICollection<string> commitsInRepo);
public record AnalyzedRepoUpdateDTO(string RepositoryIdString, DateTime State, ICollection<string> commitsInRepo);

