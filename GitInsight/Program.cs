namespace GitInsight;

using System.Security.Cryptography;
using System.Text;
using LibGit2Sharp;
using CommandLine;
using System;

public class Program{


public class Options{
    [Option('o', "repoPath", Required=true, HelpText="Enter a path to your repository located on your local device:")]
    public string RepoPath { get; set;} = null!;

    [Option('a', "authorMode", HelpText="insert a for authorMode")]
    public bool AuthorMode {get;set;}
    }

    public static async Task Main (string[] args){

    //creating the context
    using var context = new GitInsightContext();
    var analyzedRepoRepo = new AnalyzedRepoRepository(context);
    Repository repo;
    var commitAnalyzer = new CommitAnalyzer();


    await Parser.Default.ParseArguments<Options>(args).WithParsedAsync<Options> (async w => {
        if(w.AuthorMode){
            Console.WriteLine("Choose authormode with repo path: " + w.RepoPath);
            //repo = new Repository(Repository.Clone(w.RepoPath,"./testFolder"));
            //repo = new Repository(o.RepoPath.Split("https://github.com/").Last());
            // var allCommits = await analyzedRepoRepo.findCommitsInRepoAsync(repo);
            // Console.WriteLine("AUTHORMODE SELECTED");
            // commitAnalyzer.getFrequencyAuthorMode(allCommits).ToList().ForEach(Console.WriteLine);
        } else {
            Console.WriteLine("Choose without authormode with repo path: " + w.RepoPath);
            
            //repo = new Repository(Repository.Clone(w.RepoPath,"./testFolder"));
            //repo = new Repository(o.RepoPath.Split("https://github.com/").Last());
            // var allCommits = await analyzedRepoRepo.findCommitsInRepoAsync(repo);
            // Console.WriteLine("DEFAULT MODE SELECTED");
            // commitAnalyzer.getFrequency(allCommits).ToList().ForEach(Console.WriteLine);
        }
    });
    }
}

// to run do this: dotnet run -o https://github.com/monicahardt/testingRepo
