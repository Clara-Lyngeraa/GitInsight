namespace GitInsight.Entities;

public class CommmitSignature {

    [Required]
    public int RepositoryId {get;set;}

    public int CommitId {get;set;}

    [Required]
    public string? Name {get; set;}
    [Required]
    public string? Email {get; set;}
    [Required]
    public DateTimeOffset Date {get;set;}

}