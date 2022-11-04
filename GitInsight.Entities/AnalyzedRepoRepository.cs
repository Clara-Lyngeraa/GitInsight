namespace GitInsight.Entities;

public class AnalyzedRepoRepository: IDisposable{

private GitInsightContext _context;
    public AnalyzedRepoRepository(GitInsightContext context){
        this._context = context;
    }

   public (Response response, int id) Create (AnalyzedRepoCreateDTO analyzedRepo)
   {
        //finding the commitSignatures in the database that belongs to those in the analyzes repos list of names of the commits
        var commitSignatures = analyzedRepo.commitsInRepo.Select(c => FindOrCreateCommitSignature(c)).ToList();
        var newAnalyzedRepo = new AnalyzedRepo();
        newAnalyzedRepo.RepositoryIdString = analyzedRepo.RepositoryIdString;
        newAnalyzedRepo.State = analyzedRepo.State;
        newAnalyzedRepo.CommitsInRepo = commitSignatures;
        
        _context.AnalyzedRepos.Add(newAnalyzedRepo);
        _context.SaveChanges();
        

        return (Response.Created, newAnalyzedRepo.Id);
    }
    
    private DataCommit FindOrCreateCommitSignature(string Name)
    {
        var commitSigInDB = _context.Signatures.Where(c => c.Name!.Equals(Name));
        if (commitSigInDB.Any()) return commitSigInDB.First();

        return new DataCommit(name: Name);
    }



        //find already existing Datacommits in the database from their names (for now) 
        //if they are not there the method above creates a DataCommit from the given string
        //update the analyzedRepos list of DataCommits and save the changes
    public Response Update(AnalyzedRepoUpdateDTO analyzedRepo){
        
        //check if the repo exists in the database
        AnalyzedRepo currentAnalyzedRepo = _context.AnalyzedRepos.Find(analyzedRepo.RepositoryIdString)!;

        if(currentAnalyzedRepo is null){
            return Response.NotFound;
        }

        var newCommitsInRepo = analyzedRepo.commitsInRepo.Select(c => FindOrCreateCommitSignature(c)).ToList();
        currentAnalyzedRepo.CommitsInRepo = newCommitsInRepo;

        _context.SaveChanges();

        //update the list of CommitSignatures in the analyzesRepo
        return Response.Updated;
    }

    public void Dispose(){
        _context.Dispose();
    }    
}