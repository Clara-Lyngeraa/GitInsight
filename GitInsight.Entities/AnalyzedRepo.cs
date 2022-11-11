namespace GitInsight.Entities;
public class AnalyzedRepo {

    public int Id{get;set;}

   
   // public string RepositoryIdString {get;set;}

    [Required]
    [Key]
    public string Path {get;set;} = null!;

    //the last commit in the analysis
    public DateTime State {get;set;}


    public ICollection<DataCommit>? CommitsInRepo {get;set;}

}