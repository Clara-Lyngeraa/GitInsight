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
    var analyzedRepoRepo = new AnalyzedRepoRepository(context);
    var repo = new Repository(input.Value.RepoPath);
   

    var allCommits = analyzedRepoRepo.findCommitsInRepo(repo);

    if(input.Value.AuthorMode){
            analyzedRepoRepo.getFrequencyAuthorMode(allCommits).ToList().ForEach(Console.WriteLine);
        } else {
            analyzedRepoRepo.getFrequency(allCommits).ToList().ForEach(Console.WriteLine);
        }
    }
}
