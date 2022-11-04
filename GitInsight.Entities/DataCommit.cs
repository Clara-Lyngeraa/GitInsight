namespace GitInsight.Entities;

public class DataCommit {

    public int Id{get;set;}

    [Required]
    public string? RepositoryId {get;set;}

    [Required]
    public string? Name {get; set;}
    
    [Required]
    public string? Email {get; set;}
    [Required]
    public DateTimeOffset Date {get;set;}

    // public DataCommit(string name){
    //     Name = name;
    // }
}