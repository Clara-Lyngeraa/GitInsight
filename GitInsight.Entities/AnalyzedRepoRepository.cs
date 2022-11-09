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
       

        if(currentAnalyzedRepo is null){
            //calling create if the repository is not in the database
            var createDTO = new AnalyzedRepoCreateDTO(repo);
            CreateAsync(createDTO);
        }

        currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;

        if(!repoIsUpToDate(repo,currentAnalyzedRepo)){
            
          
            var updateDTO = new AnalyzedRepoUpdateDTO(repo);
            //if it is not up to date we want to call update
            UpdateAsync(updateDTO);

            currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;

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

   public async Task<(Response,AnalyzedRepoDTO)> CreateAsync (AnalyzedRepoCreateDTO analyzedRepoDTO)
   {

    var analyzedRepo = await _context.AnalyzedRepos.FirstOrDefaultAsync(c => c.Path == analyzedRepoDTO.Path);
    Response response; 

    if(analyzedRepo == null){

            analyzedRepo = new AnalyzedRepo{
            State = analyzedRepoDTO.State,
            CommitsInRepo = analyzedRepoDTO.CommitsInRepo,
            Path = analyzedRepoDTO.Path
        };

        _context.AnalyzedRepos.Add(analyzedRepo);
        await _context.SaveChangesAsync();

        response = Response.Created;
    } else {
        response = Response.Conflict;
    }

        var created = new AnalyzedRepoDTO(analyzedRepo.Id, analyzedRepo.Path, analyzedRepo.State, analyzedRepo.CommitsInRepo);
        //analyzedRepo.CommitsInRepo.ToList().ForEach(c => c.Repo = analyzedRepo);
        
        return (response, created);
    }
    
   
        //find already existing Datacommits in the database from their names (for now) 
        //if they are not there the method above creates a DataCommit from the given string
        //update the analyzedRepos list of DataCommits and save the changes
    public async Task<Response> UpdateAsync(AnalyzedRepoUpdateDTO updateDTO){
        var repoInDB = await _context.AnalyzedRepos.FindAsync(updateDTO.Path);

        if(repoInDB == null){
            Console.WriteLine("WTF");
        }

        var listSorted = updateDTO.CommitsInRepo.OrderBy(c => c.Date);
       
        var sortedRepoCommits = updateDTO.CommitsInRepo.Where(c => c.Date > repoInDB.State).Select(c => new DataCommit{
            StringId = c.StringId,
            Name = c.Name,
            Date = c.Date
        });

        if(sortedRepoCommits.ToList().Count()!=0){
            foreach(DataCommit dc in sortedRepoCommits){
                repoInDB.CommitsInRepo.Add(dc);
            }
            
            repoInDB.State = sortedRepoCommits.Last().Date;
        }
        _context.SaveChanges();

        //update the list of CommitSignatures in the analyzesRepo
        return Response.Updated;
    }


    public void Dispose(){
        _context.Dispose();
    }    
}