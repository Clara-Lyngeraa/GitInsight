using System.Security.Cryptography;
using System.Text;

namespace GitInsight.Entities.DTOs;


public static class Utilities{
  public static string getRepoHashedID(Repository repo){
    //var sortedCommits = repo.Commits.OrderBy(c => c.Author.When.Date);
      string firstCommitToHash = "hej";
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
        return firstCommitToHash;
    }

}