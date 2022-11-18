
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

      public  IEnumerable<Fork> GetForkList()
    {
       
        IEnumerable<Fork>? forks = new List<Fork>();
        return forks;
    }


}