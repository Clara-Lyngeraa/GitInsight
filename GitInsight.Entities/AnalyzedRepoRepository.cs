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
        AnalyzedRepo currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;
        Console.WriteLine("current analyzed repo is first: " + currentAnalyzedRepo);

        if(currentAnalyzedRepo is null){
            Console.WriteLine("Found a repo for the first time");
            //calling create if the repository is not in the database
            var createDTO = new AnalyzedRepoCreateDTO(repo);
            Create(createDTO);
        }

        currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;
        Console.WriteLine("current analyzed repo is second: " + currentAnalyzedRepo);

        if(!repoIsUpToDate(repo,currentAnalyzedRepo)){
            Console.WriteLine("repo was not up to date");
          
            var updateDTO = new AnalyzedRepoUpdateDTO(repo);
            Console.WriteLine("Calling update with update dto: " + updateDTO.Path);
            //if it is not up to date we want to call update
            Update(updateDTO);

            currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;

            Console.WriteLine("after update the repo contains this many datacommits: " + currentAnalyzedRepo.CommitsInRepo.Count());
        }
         
       
        foreach(DataCommit dc in currentAnalyzedRepo.CommitsInRepo){
            Console.WriteLine(dc.Date);
            yield return dc;
        }
    }

    public bool repoIsUpToDate(Repository repo, AnalyzedRepo dbRepo){
        return dbRepo.State == getTimeOfLastestCommit(repo);
    }

   public DateTime getTimeOfLastestCommit(Repository repo){
        return repo.Commits.Last().Author.When.Date;
   }

   public AnalyzedRepo Create (AnalyzedRepoCreateDTO analyzedRepoDTO)
   {
        var newAnalyzedRepo = new AnalyzedRepo{
            //RepositoryIdString = analyzedRepoDTO.RepositoryIdString,
            State = analyzedRepoDTO.State,
            CommitsInRepo = analyzedRepoDTO.CommitsInRepo,
            Path = analyzedRepoDTO.Path
        };

        newAnalyzedRepo.CommitsInRepo.ToList().ForEach(c => c.Repo = newAnalyzedRepo);
    
        _context.AnalyzedRepos.Add(newAnalyzedRepo);
        _context.SaveChanges();
        
        return newAnalyzedRepo;
    }
    
   
        //find already existing Datacommits in the database from their names (for now) 
        //if they are not there the method above creates a DataCommit from the given string
        //update the analyzedRepos list of DataCommits and save the changes
    public Response Update(AnalyzedRepoUpdateDTO updateDTO){
        var repoInDB = _context.AnalyzedRepos.Find(updateDTO.Path);
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
        
        repoInDB.State = sortedRepoCommits.Last().Date;
        _context.SaveChanges();

        //update the list of CommitSignatures in the analyzesRepo
        return Response.Updated;
    }


    public void Dispose(){
        _context.Dispose();
    }    
}