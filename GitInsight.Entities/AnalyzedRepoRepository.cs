namespace GitInsight.Entities;

public class AnalyzedRepoRepository: IDisposable{

private GitInsightContext _context;
    public AnalyzedRepoRepository(GitInsightContext context){
        this._context = context;
    }

   public (Response response, string id) Create (AnalyzedRepoCreateDTO analyzedRepo)
   {
        //checking if it already exist:
        AnalyzedRepo repo = _context.AnalyzedRepos.Find(analyzedRepo.RepositoryIdString);
        
        if(repo != null){
            Console.WriteLine("Create was called with already in db repo");
            return (Response.Created, repo.RepositoryIdString);
        }

        //finding the commitSignatures in the database that belongs to those in the analyzes repos list of names of the commits
        List<DataCommit> commitSignatures = analyzedRepo.commitsInRepo.Select(c => FindOrCreateDataCommit(c)).ToList();

        var newAnalyzedRepo = new AnalyzedRepo();
        newAnalyzedRepo.RepositoryIdString = analyzedRepo.RepositoryIdString;
        newAnalyzedRepo.State = analyzedRepo.State;
        newAnalyzedRepo.CommitsInRepo = commitSignatures;
        
        _context.AnalyzedRepos.Add(newAnalyzedRepo);
        _context.SaveChanges();
        
        return (Response.Created, newAnalyzedRepo.RepositoryIdString);
    }
    
    private DataCommit FindOrCreateDataCommit(string commitId)
    {
        var dataCommitInDB = _context.DataCommits.Where(c => c.StringId.Equals(commitId));
        if (dataCommitInDB.Any()) return dataCommitInDB.First();

        return new DataCommit{
            StringId = commitId,
        };
    }

        //find already existing Datacommits in the database from their names (for now) 
        //if they are not there the method above creates a DataCommit from the given string
        //update the analyzedRepos list of DataCommits and save the changes
    public Response Update(AnalyzedRepoUpdateDTO analyzedRepo){
        
        //check if the repo exists in the database
        AnalyzedRepo currentAnalyzedRepo = _context.AnalyzedRepos.Find(analyzedRepo.RepositoryIdString);

        if(currentAnalyzedRepo is null){
            return Response.NotFound;
        }

        //if the repo is already up to date we want to stop here
        if(analyzedRepo.State == currentAnalyzedRepo.State){
            return Response.UpToDate;
        }

        var newCommitsInRepo = analyzedRepo.commitsInRepo.Select(c => FindOrCreateDataCommit(c)).ToList();
        currentAnalyzedRepo.CommitsInRepo = newCommitsInRepo;

        _context.SaveChanges();

        //update the list of CommitSignatures in the analyzesRepo
        return Response.Updated;
    }

     public bool FindWithId(int repoId)
    {
        var repo = _context.AnalyzedRepos.Find(repoId);
        return repo != null ? true : false;

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