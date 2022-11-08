namespace GitInsight.Entities;
public class AnalyzedRepo {

    //public int Id{get;set;}

    [Required]
    [Key]
    public string? RepositoryIdString {get;set;}

    //the last commit in the analysis
    public DateTime State {get;set;}

    public ICollection<DataCommit>? CommitsInRepo {get;set;}

}