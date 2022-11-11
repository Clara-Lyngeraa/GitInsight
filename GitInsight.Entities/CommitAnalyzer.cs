
namespace GitInsight.Entities;

public class CommitAnalyzer{

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
             yield return string.Empty;
        }
       
    }


//         //THIS METHOD SHOULD ONLY TAKE A REPO STRING
//     //WE HAVE A PROBLEM IF WE MAKE THE REEPO IN THE PROGRAM. I DO NOT THINK THAT THIS IS POSSIBLE
//     //REQUIRES EXTERN AUTHENTICATION AND A CLONE
//     public async Task<IEnumerable<DataCommit>> findCommitsInRepoAsync(Repository repo){

//         //calling create 
//         //create has check if it is already in the db, if not it creates it
//         var createDTO = new AnalyzedRepoCreateDTO(repo);
//         var (response, dto) = await CreateAsync(createDTO);
        
//         //if(response != Response.Conflict){
//         var currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;

//         //checking if it is up to date
//         if(!repoIsUpToDate(repo,currentAnalyzedRepo)){
//             var updateDTO = new AnalyzedRepoUpdateDTO(repo);
//             //if it is not up to date we want to call update
//             UpdateAsync(updateDTO);

//             //we find the updates version here
//             currentAnalyzedRepo = _context.AnalyzedRepos.Find(repo.Info.Path)!;
//         }
       
//         // foreach(DataCommit dc in currentAnalyzedRepo.CommitsInRepo){
//         //     Console.WriteLine(dc.Date);
//         //     yield return dc;
//         // }
//         return currentAnalyzedRepo.CommitsInRepo;
//     }

//     //if we make a class that calls create and update. Then we enter the commit section of the database to find these commits. 
//     //in create method we then make a repository and add all the commits to the commit section. The method find all commits 
//     //should be in the datacommitrepository

//     public bool repoIsUpToDate(Repository repo, AnalyzedRepo dbRepo){
//         return dbRepo.State == getTimeOfLastestCommit(repo);
//     }

//    public DateTime getTimeOfLastestCommit(Repository repo){
//         return repo.Commits.Last().Author.When.Date;
//    }


}