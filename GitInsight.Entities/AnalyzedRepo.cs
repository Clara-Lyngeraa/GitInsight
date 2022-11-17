namespace GitInsight.Entities;
public class AnalyzedRepo {

    [Required]
    [Key]
    public int Id{get;set;}


    public string Path {get;set;} = null!;

    //the last commit in the analysis
    public DateTime State {get;set;}


    public ICollection<DataCommit>? CommitsInRepo {get;set;}

}