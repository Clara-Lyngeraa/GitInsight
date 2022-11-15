using System.Security.Cryptography;
using System.Text;
using GitInsight.Entities.DTOs;

namespace GitInsight.Entities.DTOs;

public record AnalyzedRepoDTO(int Id, string path, DateTime State, ICollection<DataCommit> commitsInRepo);
public record AnalyzedRepoUpdateDTO{
 public string RepositoryIdString;
    public DateTime State;
    public ICollection<DataCommit> CommitsInRepo;
    public string Path;
    public AnalyzedRepoUpdateDTO(Repository repo)
    {
        //RepositoryIdString = Utilities.getRepoHashedID(repo);  
        State = repo.Commits.Last().Author.When.Date;
        Path = repo.Info.Path;
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
    public DateTime State;
    public ICollection<DataCommit> CommitsInRepo = null!;
    public string Path;
    public AnalyzedRepoCreateDTO(Repository repo)
    {
        var list = repo.Commits.OrderBy(c => c.Author.When.Date);
        
        Path = repo.Info.Path;
        CommitsInRepo = new List<DataCommit>();
        if(list.Count() > 0){
             State = list.Last().Author.When.Date;
        } else {
            State = DateTime.Now;
        }
       
        
        
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


