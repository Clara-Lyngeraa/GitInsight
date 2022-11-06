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
   } else {
        //do not analyze with authormode
        //analyze(repo,context);
   }
}


public static void analyze(Repository repoToAnalyze, GitInsightContext context){

    AnalyzedRepoRepository analyzedRepoRepo = new AnalyzedRepoRepository(context);
    
    //get latest commit here
    DateTime stateOfRepoToAnalyze = repoToAnalyze.Commits.Last().Author.When.DateTime;

    List<string> commits = repoToAnalyze.Commits.Select(c => c.Id.ToString()).ToList();
    var reponse = analyzedRepoRepo.Update(new AnalyzedRepoUpdateDTO(getRepoHashedID(repoToAnalyze),stateOfRepoToAnalyze,commits));
    
    //if the repo to be analyzed is not in the database we create it
    if(reponse == Response.NotFound){
        var createDTO = new AnalyzedRepoCreateDTO(getRepoHashedID(repoToAnalyze),stateOfRepoToAnalyze,commits);
        var (response, id) = analyzedRepoRepo.Create(createDTO);

   
    foreach(string s in getFrequence(repoToAnalyze,id,context)){
        Console.WriteLine(s);
    }
    } 
}

    public static IEnumerable<string> getFrequence(Repository repo, int idOfRepo, GitInsightContext context)
    {
         //THIS IS NOT THE WAY TO DO THIS BUT I DO NOT KNOW HOW!!!!!!!!!!!!
        foreach(Commit c in repo.Commits){
        var commitToUpdateWithDate = context.DataCommits.Find(c.Id.ToString());
        commitToUpdateWithDate.Date = c.Author.When.Date;
        context.SaveChanges();
    }
        //in the context we want to find the repository and then count the number of commits per day
        var analyzedRepo = context.AnalyzedRepos.Find(idOfRepo);
        
        return from c in analyzedRepo.CommitsInRepo
        .GroupBy(c => c.Date.ToShortDateString().ToString())
                //group on the date
                //count how many rows
                let amount = c.Count()
                let date = c.First().Date.ToShortDateString().ToString()
                select $"{amount} {date}";
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
