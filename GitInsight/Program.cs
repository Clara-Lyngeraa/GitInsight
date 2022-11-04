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
       //do stuff
    } else if(input == "-f"){
        //do stuff
    } else {
         throw new ArgumentException("Enter valid mode");
     }
}


public static void analyze(Repository repoToAnalyze, GitInsightContext context){

    AnalyzedRepoRepository repo = new AnalyzedRepoRepository(context);
    

    //check in the database if the repository to be analyzed has already been analyzed
    AnalyzedRepo repoToUpdate = context.AnalyzedRepos.Find(getRepoHashedID(repoToAnalyze))!;

     //if it is not already in the database we need to add an analysis
        if(repoToUpdate == null){
            Console.WriteLine("the repo was not already in the database and has therefor not been analyzed.. yet");
            DateTime stateOfRepoToAnalyze = repoToAnalyze.Commits.Last().Author.When.DateTime;
            //call create method with a createDTO to add the repo to the database
            var createDTO = new AnalyzedRepoCreateDTO(getRepoHashedID(repoToAnalyze),stateOfRepoToAnalyze,repoToAnalyze.Commits.Select(c => c.Author.Name).ToList());
            repo.Create(createDTO);
            
        } else {
            Console.WriteLine("the repo was  already in the database we need to return it or update it");
            //have a check if there are any new commits
            //we do so by checking the date of the last commit in the repo we want to analyse
            DateTime stateOfRepoToAnalyze = repoToAnalyze.Commits.Last().Author.When.DateTime;
            //If State (last commit in the analysis) of the analyzed repository is the same as the repository we are trying to 
            //analyse now we do not want to update
            

            if(stateOfRepoToAnalyze == repoToUpdate.State){
                //want to return what is already in the database
            } else {
                //call update method
            }
        }

}

private static string getRepoHashedID(Repository repo){
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
//     //idea here: in frequence mode: a dictionary that maps an integer to a date
//     //every time the date is "read" in the commitlog, the integer is incremented. 
//     //doing this for all commits in the log
//     //returning the dictionary

//     //the below method can also look like this
//      //which method is better?
//             // if(!dic.ContainsKey(c.Author.When)){
//             //       dic.Add(c.Author.When,1);
//             // } else {
//             //      dic[c.Author.When]++;
//             // }
// public static Dictionary<string,int> getFrequence(Repository repo)
//     {
//         Dictionary<string,int> dic = new Dictionary<string,int>{};
//         foreach(Commit c in repo.Commits){
                
//             string date = c.Author.When.Date.ToShortDateString().ToString();
//             if(!dic.ContainsKey(date)){
//                 dic.Add(date,1);
//             } else {
//                  dic[date]++;
//             }
//         }
//         printFrequency(dic);
//         return dic;  
//     }


//     //same as above but in author mode. Maybe seperate dictionaries per person, i'm not sure
//     //incrementing the count whenever the same date is "read"
//     public static Dictionary<string,Dictionary<string,int>> getFrequenceAuthorMode(Repository repo)
//     {
//         Dictionary<string,Dictionary<string,int>> DicOfDic = new Dictionary<string,Dictionary<string,int>>{};

//         foreach(Commit c in repo.Commits){
//             string nameOfCommitAuthor = c.Author.Name.ToString();
//             string dateOfCommit = c.Author.When.Date.ToShortDateString().ToString();
            
//             if(!DicOfDic.ContainsKey(nameOfCommitAuthor)){
//                 //if the author does not already have a dictionary
//                 Dictionary<string,int> newAuthor = new Dictionary<string, int>{};
//                 DicOfDic.Add(nameOfCommitAuthor,newAuthor);
//                 newAuthor.Add(dateOfCommit,1);
                
//             } else {
//                 //if the author already have a dictionary we want to insert in that one
//                 Dictionary<string,int> authorsDic = DicOfDic[nameOfCommitAuthor];
//                 if(!authorsDic.ContainsKey(dateOfCommit)){
//                 authorsDic.Add(dateOfCommit,1);
//             } else {
//                  authorsDic[dateOfCommit]++;
//             }
//             }
//         }
//         printFrequencyAuthorMode(DicOfDic);
//         return DicOfDic;
//     }


//     //we also want a printmethod to print to the terminal
//     public static void printFrequency(Dictionary<string,int> dicToPrint){

//         var list = dicToPrint.Keys.ToList();
//         list.Sort();

//         foreach(string key in list){
//             Console.WriteLine(dicToPrint[key].ToString() + " " +  key);
//         }

//         //unsorted but faster
//         // foreach(KeyValuePair<DateTimeOffset,int> entry in dicToPrint){
//         //     Console.WriteLine(entry.Value.ToString() + " " + entry.Key.Date.ToShortDateString().ToString());
//         // }
//     }

//     public static void printFrequencyAuthorMode(Dictionary<string,Dictionary<string,int>> dicToDicToPrint){
//         foreach(KeyValuePair<string,Dictionary<string,int>> authorDic in dicToDicToPrint){
//             Console.WriteLine(authorDic.Key); //should print the name of the author
//             printFrequency(authorDic.Value);
//             Console.WriteLine("");
//         }

//     }
// }


//added a new signature to the database test
    // var sig = new GitInsight.Entities.CommmitSignature{Name="Monica",Email="test@itu.dk",Date=new DateTimeOffset()};
    // context.Signatures.Add(sig);
    // context.SaveChanges();
}