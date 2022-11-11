namespace GitInsight.Entities;

public class DataCommit {

    [Required]
    [Key]
    public string StringId {get;set;}

    public string? Name {get; set;}
      
     [Required]
    public DateTime Date {get;set;}

    //public AnalyzedRepo Repo {get; set;}

    
}