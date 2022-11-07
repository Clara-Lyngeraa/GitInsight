using System.Security.Cryptography;
using System.Text;

namespace GitInsight.Entities;

public class AnalyzedRepoRepository: IDisposable{

private GitInsightContext _context;
    public AnalyzedRepoRepository(GitInsightContext context){
        this._context = context;
    }

    public IEnumerable<DataCommit> findCommitsInRepo(Repository repo){
        AnalyzedRepo currentAnalyzedRepo = _context.AnalyzedRepos.Find(getRepoHashedID(repo));

        if(currentAnalyzedRepo is null){
            //calling create if the repository is not in the database
            List<string> commits = repo.Commits.Select(c => c.Id.ToString()).ToList();
            var createDTO = new AnalyzedRepoCreateDTO(getRepoHashedID(repo),getTimeOfLastestCommit(repo),commits);
            Create(createDTO);

            currentAnalyzedRepo = _context.AnalyzedRepos.Find(getRepoHashedID(repo));

            //HOW DO WE AVOID THIS???
            giveCommitsTheirDatesAndNames(repo);
        }

        if(!repoIsUpToDate(repo,currentAnalyzedRepo)){
            //if it is not up to date we want to call update
            Update(repo);
        }

        foreach(DataCommit dc in currentAnalyzedRepo.CommitsInRepo){
            yield return dc;
        }
    }

    public void giveCommitsTheirDatesAndNames(Repository repo)
    {
        //THIS IS NOT WHERE TO DO THIS BUT WHERE DO WE GIVE THEM THEIR DATES????? 
            foreach(Commit c in repo.Commits){
            var commitToUpdateWithDate = _context.DataCommits.Find(c.Id.ToString());
             commitToUpdateWithDate.Date = c.Author.When.Date;
             commitToUpdateWithDate.Name = c.Author.Name;
            _context.SaveChanges();
        }  
    }

    public DataCommit DataCommitFromCommit(Commit c) => new DataCommit
    {
        StringId = c.Id.ToString(),
        Name = c.Author.Name,
        Date = c.Author.When.Date
    };
          

      //NOT TESTED YET
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

   public Response Create (AnalyzedRepoCreateDTO analyzedRepo)
   {
        //finding the commitSignatures in the database that belongs to those in the analyzes repos list of names of the commits
        List<DataCommit> commitSignatures = analyzedRepo.commitsInRepo.Select(c => FindOrCreateDataCommit(c)).ToList();

        var newAnalyzedRepo = new AnalyzedRepo();
        newAnalyzedRepo.RepositoryIdString = analyzedRepo.RepositoryIdString;
        newAnalyzedRepo.State = analyzedRepo.State;
        newAnalyzedRepo.CommitsInRepo = commitSignatures;
        
        _context.AnalyzedRepos.Add(newAnalyzedRepo);
        _context.SaveChanges();
        
        return (Response.Created);
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
    public Response Update(Repository repo){
        //maybe check for new commits
        var repoInDB = _context.AnalyzedRepos.Find(getRepoHashedID(repo));
        var sortedRepoCommits = repo.Commits.OrderBy(c => c.Author.When.Date);

        foreach(Commit c in sortedRepoCommits){
            if(c.Author.When.Date > repoInDB.State){
                //if this is true the commit is new
                repoInDB.CommitsInRepo.Add(new DataCommit{
                    StringId = c.Id.ToString(),
                    Name = c.Author.Name,
                    Date = c.Author.When.Date
                });
            }
        }
        repoInDB.State = sortedRepoCommits.Last().Author.When.Date;
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


    public IEnumerable<string> getFrequency(IEnumerable<DataCommit> commits)
    {
        //in the context we want to find the repository and then count the number of commits per day
        //AnalyzedRepo analyzedRepo = context.AnalyzedRepos.Find(getRepoHashedID(repo));
        return from c in commits
        .GroupBy(c => c.Date.ToShortDateString().ToString())
                //group on the date
                //count how many rows
                let amount = c.Count()
                let date = c.First().Date.ToShortDateString().ToString()
                select $"{amount} {date}";
        }


    public IEnumerable<string> getFrequencyAuthorMode(IEnumerable<DataCommit> commits)
    {
        //group on name on the result of the method above
        foreach(var commitAuthor in commits.GroupBy(c => c.Name).Distinct()){
            yield return commitAuthor.First().Name;

            foreach(var commit in getFrequency(commitAuthor)){
                yield return commit;
            }
        }
    }
}