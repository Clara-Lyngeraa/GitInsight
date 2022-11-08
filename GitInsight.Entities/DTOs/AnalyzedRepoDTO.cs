using System.Security.Cryptography;
using System.Text;
using GitInsight.Entities.DTOs;

namespace GitInsight.Entities.DTOs;

public record AnalyzedRepoDTO(int Id, string RepositoryIdString, DateTime State, ICollection<DataCommit> commitsInRepo);
public record AnalyzedRepoUpdateDTO{
 public string RepositoryIdString;
    public DateTime State;
    public ICollection<DataCommit> CommitsInRepo;
    public AnalyzedRepoUpdateDTO(Repository repo)
    {
        RepositoryIdString = Utilities.getRepoHashedID(repo);  
        //Console.WriteLine(RepositoryIdString);
        State = repo.Commits.Last().Author.When.Date;
        CommitsInRepo = new List<DataCommit>();;
        
        foreach(Commit c in repo.Commits)
        {
            CommitsInRepo.Add(new DataCommit{
            StringId = c.Id.ToString(),
            Name = c.Author.Name,
            Date = c.Author.When.Date
            });
        }
    }
}

public record AnalyzedRepoCreateDTO
{
    public string RepositoryIdString;
    public DateTime State;
    public ICollection<DataCommit> CommitsInRepo = null!;
    public AnalyzedRepoCreateDTO(Repository repo)
    {
        RepositoryIdString = Utilities.getRepoHashedID(repo);  
        //Console.WriteLine(RepositoryIdString);

        var list = repo.Commits.OrderBy(c => c.Author.When.Date);
        State = list.Last().Author.When.Date;
        CommitsInRepo = new List<DataCommit>();;
        
        foreach(Commit c in repo.Commits)
        {
            CommitsInRepo.Add(new DataCommit{
            StringId = c.Id.ToString(),
            Name = c.Author.Name,
            Date = c.Author.When.Date
            });
        }
    }

}


