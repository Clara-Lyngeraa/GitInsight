namespace GitInsight.Entities;

public class DataCommit {

    //public int Id {get;set;}

    [Required]
    [Key]
    public string StringId {get;set;}

    
    //public string? RepositoryId {get;set;}

    
    public string? Name {get; set;}
    

    //public string? Email {get; set;}
  
     [Required]
    public DateTime Date {get;set;}

    
}