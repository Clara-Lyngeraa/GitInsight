using System.Security.Cryptography;
using System.Text;

namespace GitInsight.Entities.DTOs;

public record AnalyzedRepoDTO(int Id, string RepositoryIdString, DateTime State, ICollection<DataCommit> commitsInRepo);
public record AnalyzedRepoUpdateDTO(string RepositoryIdString, DateTime State, ICollection<DataCommit> CommitsInRepo);

public record AnalyzedRepoCreateDTO
{
    public string RepositoryIdString;
    public DateTime State;
    public ICollection<DataCommit> CommitsInRepo;
    public AnalyzedRepoCreateDTO(Repository repo)
    {
        RepositoryIdString = getRepoHashedID(repo);  
        State = DateTime.Now;
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
    public static string getRepoHashedID(Repository repo){
      string firstCommitToHash = repo.Commits.First().Author + repo.Commits.First().Message;
      string hashedRepo = string.Empty;
      using (SHA256 sha256 = SHA256.Create())
        {
            // Compute the hash of the given string
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(firstCommitToHash));
 
            // Convert the byte array to string format
            foreach (byte b in hashValue) {
                hashedRepo += $"{b:X2}";
            }
        }
        return hashedRepo;
    }
}


