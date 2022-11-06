namespace GitInsight;

using System.Security.Cryptography;
using System.Text;
using LibGit2Sharp;
using CommandLine;
using System;

public class Program{


public class Options{
    [Option('f', "repoPath", Required=true, HelpText="Enter a path to your repository located on your local device:")]
    public string RepoPath { get; set;} = null!;

    [Option('a', "authorMode", HelpText="insert a for authorMode")]
    public bool AuthorMode {get;set;}
    }

    public static void Main (string[] args){

    //creating the context
    using var context = new GitInsightContext();
    
    var input = Parser.Default.ParseArguments<Options>(args);
    var repo = new Repository(input.Value.RepoPath);
   
   if(input.Value.AuthorMode){
    //analyze with authormode
    analyze(repo,context,true);
   } else {
        //do not analyze with authormode
    
    analyze(repo,context,false);
   }
}

public static void analyze(Repository repoToAnalyze, GitInsightContext context, Boolean author){

    AnalyzedRepoRepository analyzedRepoRepo = new AnalyzedRepoRepository(context);
    

    DateTime stateOfRepoToAnalyze = repoToAnalyze.Commits.Last().Author.When.DateTime;
    List<string> commits = repoToAnalyze.Commits.Select(c => c.Id.ToString()).ToList();

    //calling create
    var createDTO = new AnalyzedRepoCreateDTO(getRepoHashedID(repoToAnalyze),stateOfRepoToAnalyze,commits);
    analyzedRepoRepo.Create(createDTO);

    //calling update
    analyzedRepoRepo.Update(new AnalyzedRepoUpdateDTO(getRepoHashedID(repoToAnalyze),stateOfRepoToAnalyze,commits));
    
    if(author){
        getFrequencyAuthorMode(repoToAnalyze,context).ToList().ForEach(Console.WriteLine);
    } else {
        getFrequency(repoToAnalyze,context).ToList().ForEach(Console.WriteLine);
    }
}

    public static IEnumerable<string> getFrequency(Repository repo, GitInsightContext context)
    {
         //THIS IS NOT THE WAY TO DO THIS BUT I DO NOT KNOW HOW!!!!!!!!!!!!
        foreach(Commit c in repo.Commits){
        var commitToUpdateWithDate = context.DataCommits.Find(c.Id.ToString());
        commitToUpdateWithDate.Date = c.Author.When.Date;
        context.SaveChanges();
    }
        //in the context we want to find the repository and then count the number of commits per day
        AnalyzedRepo analyzedRepo = context.AnalyzedRepos.Find(getRepoHashedID(repo));
        
        return from c in analyzedRepo.CommitsInRepo
        .GroupBy(c => c.Date.ToShortDateString().ToString())
                //group on the date
                //count how many rows
                let amount = c.Count()
                let date = c.First().Date.ToShortDateString().ToString()
                select $"{amount} {date}";
        }
    public static IEnumerable<string> getFrequencyAuthorMode(Repository repo, GitInsightContext context)
    {
         //THIS IS NOT THE WAY TO DO THIS BUT I DO NOT KNOW HOW!!!!!!!!!!!!
        foreach(Commit c in repo.Commits){
        var commitToUpdateWithDate = context.DataCommits.Find(c.Id.ToString());
        commitToUpdateWithDate.Date = c.Author.When.Date;
        commitToUpdateWithDate.Name = c.Author.Name;
        context.SaveChanges();
    }
        //in the context we want to find the repository and then count the number of commits per day
        AnalyzedRepo analyzedRepo = context.AnalyzedRepos.Find(getRepoHashedID(repo));
        
        //group on name on the result of the method above

        foreach(var commitAuthor in analyzedRepo.CommitsInRepo.GroupBy(c => c.Name)){
            yield return commitAuthor.First().Name;

            foreach(var commit in commitAuthor.GroupBy(c => c.Date.ToShortDateString().ToString())){
                var amount = commit.Count();
                var dated = commit.First().Date.ToShortDateString().ToString();
                yield return amount + " " + dated;
            }
        }
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
}
