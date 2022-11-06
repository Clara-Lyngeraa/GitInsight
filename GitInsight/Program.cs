namespace GitInsight;

using System.Security.Cryptography;
using System.Text;
using LibGit2Sharp;

public class Program{


public static void Main (string[] args){

    //creating the context
    using var context = new GitInsightContext();
    
    //in the terminal we want to check if agrs[0] is either something like -f or -a given this information to the user. 
    //if it input is -f run in frequency mode
    //if the input is -a run in author mode

    Console.Write("Enter a path to your repository located on your local device: ");
    var pathToRepo = Console.ReadLine();

    var repo = new Repository(pathToRepo);
   
	Console.Write("-a for authormode -f for freqyencemode: ");
	var input = Console.ReadLine();

	//Process input
	if(input == "-a"){
       analyze(repo,context);
    } else if(input == "-f"){
        analyze(repo,context);
    } else {
         throw new ArgumentException("Enter valid mode");
     }
}


public static void analyze(Repository repoToAnalyze, GitInsightContext context){

    AnalyzedRepoRepository analyzedRepoRepo = new AnalyzedRepoRepository(context);
    
    //calling update method
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
