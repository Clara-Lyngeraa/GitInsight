namespace GitInsight.Entities;
public class AnalyzedRepo {

    public int Id{get;set;}

    public string? RepositoryIdString {get;set;}


    //the last commit in the analysis
    public DateTime State {get;set;}

    public ICollection<DataCommit> CommitsInRepo {get;set;} = new List<DataCommit>();

}