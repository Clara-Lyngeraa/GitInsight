using System.Security.Cryptography;
using System.Text;
using GitInsight.Entities.DTOs;

namespace GitInsight.Entities;

public class AnalyzedRepoRepository: IDisposable{

private GitInsightContext _context;
    public AnalyzedRepoRepository(GitInsightContext context){
        this._context = context;
    }

    public IEnumerable<DataCommit> findCommitsInRepo(Repository repo){
        AnalyzedRepo currentAnalyzedRepo = _context.AnalyzedRepos.Find(getRepoHashedID(repo))!;

        if(currentAnalyzedRepo is null){
            //calling create if the repository is not in the database
            var createDTO = new AnalyzedRepoCreateDTO(repo);
            Create(createDTO);
        }

        currentAnalyzedRepo = _context.AnalyzedRepos.Find(getRepoHashedID(repo))!;
        

        if(!repoIsUpToDate(repo,currentAnalyzedRepo)){
            //if it is not up to date we want to call update
            Update(new AnalyzedRepoUpdateDTO(repo));
        }

        foreach(DataCommit dc in currentAnalyzedRepo.CommitsInRepo){
            yield return dc;
        }
    }

    public bool repoIsUpToDate(Repository repo, AnalyzedRepo dbRepo){
        return dbRepo.State == getTimeOfLastestCommit(repo);
    }

   public DateTime getTimeOfLastestCommit(Repository repo){
        return repo.Commits.Last().Author.When.Date;
   }
  
    //hashing the repository to get a repoStringID
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

   public Response Create (AnalyzedRepoCreateDTO analyzedRepoDTO)
   {
        var newAnalyzedRepo = new AnalyzedRepo{
            RepositoryIdString = analyzedRepoDTO.RepositoryIdString,
            State = analyzedRepoDTO.State,
            CommitsInRepo = analyzedRepoDTO.CommitsInRepo,
        };

        newAnalyzedRepo.CommitsInRepo.ToList().ForEach(c => c.Repo = newAnalyzedRepo);
    
        _context.AnalyzedRepos.Add(newAnalyzedRepo);
        _context.SaveChanges();
        
        return Response.Created;
    }
    
   
        //find already existing Datacommits in the database from their names (for now) 
        //if they are not there the method above creates a DataCommit from the given string
        //update the analyzedRepos list of DataCommits and save the changes
    public Response Update(AnalyzedRepoUpdateDTO updateDTO){
        var repoInDB = _context.AnalyzedRepos.Find(updateDTO.RepositoryIdString);
        if(repoInDB == null){
            Console.WriteLine("WTF");
        }

        var listSorted = updateDTO.CommitsInRepo.OrderBy(c => c.Date);
       
        var sortedRepoCommits = updateDTO.CommitsInRepo.Where(c => c.Date > repoInDB.State).Select(c => new DataCommit{
            StringId = c.StringId,
            Name = c.Name,
            Date = c.Date
        });

        foreach(DataCommit dc in sortedRepoCommits){
            repoInDB.CommitsInRepo.Add(dc);
        }
    
        foreach(DataCommit dc in repoInDB.CommitsInRepo){
            Console.WriteLine(dc.Date);
        }
        
        repoInDB.State = sortedRepoCommits.Last().Date;
        _context.SaveChanges();

        //update the list of CommitSignatures in the analyzesRepo
        return Response.Updated;
    }

     public AnalyzedRepo FindWithStringId(string repoStringId)
    {
        var idOfFoundRepo = _context.AnalyzedRepos.Find(repoStringId);
        return idOfFoundRepo;
    }

    public void Dispose(){
        _context.Dispose();
    }    
}