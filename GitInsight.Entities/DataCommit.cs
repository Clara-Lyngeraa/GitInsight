namespace GitInsight.Entities;

public class DataCommit {

    [Required]
    [Key]
    public string StringId {get;set;} = null!;

    public string Name {get; set;} = null!;
      
     [Required]
    public DateTime Date {get;set;}

    //public AnalyzedRepo Repo {get; set;}

    
}