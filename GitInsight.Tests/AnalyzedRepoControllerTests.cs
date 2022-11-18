namespace GitInsight.Tests;

using System.Net;
using GitInsight.Server.Controllers;
using Microsoft.AspNetCore.Mvc.Core;
public class AnalyzedRepoControllerTests: IDisposable {

    private AnalyzedRepoController _controller;
    private GitInsightContext _context;
    private AnalyzedRepoRepository _repo;
    private Repository testRepo;

    public AnalyzedRepoControllerTests(){
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>();
        builder.UseSqlite(connection);

        var context = new GitInsightContext(builder.Options);
        context.Database.EnsureCreated();
        _context = context;
        _repo = new AnalyzedRepoRepository(_context);
        _controller = new AnalyzedRepoController(_repo);

        var path = Repository.Init(".");
        testRepo = new Repository(path);
    
         Signature sig1 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
         Signature sig2 = new Signature("Person2", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
         Signature sig3 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
         Signature sig4 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,11,25)));

         testRepo.Commit("Inital commit", sig1, sig1, new CommitOptions() {AllowEmptyCommit = true});
         testRepo.Commit("Inital commit1", sig2, sig2, new CommitOptions() {AllowEmptyCommit = true});
         testRepo.Commit("Inital commit2", sig3, sig3, new CommitOptions() {AllowEmptyCommit = true});
         testRepo.Commit("Inital commit3", sig4, sig4, new CommitOptions() {AllowEmptyCommit = true});


        
    }
    public void Dispose()   
    {
        _context.Dispose();
        _repo.Dispose();
        testRepo.Dispose();
        Directory.Delete("./.git",true);
        Directory.Delete("./ClonedRepo",true);
    }

    //HOW DO WE TEST THIS WITH A URL; WHEN A REPO CAN CHANGE IF A COMMIT IS MADE; AKA WE NEED TO CHANGE THE LIST RETURNED?
    //WE NEED TO COMMENT OUT LINE WHERE THE CLONED REPO FOLDEDR IS DELETED. THIS IS BECAUSE THAT FOLDER IS CREATED IN THE TESTFOLDER
    //WHEN RUNNING TESTS, AND IN SERVER ELSE. SO THE SERVER CANNOT FIND IT IF TEST IS RUN
    [Fact]
    public async void get_request_returns_analyzed_repos_commits()
    {
        //Arrange 
        var requestUrl = "https://github.com/monicahardt/testingRepo";
        requestUrl = WebUtility.UrlEncode(requestUrl);
        var authorModeOn = false;
    
        // Act
        var analysisResult = await _controller.Get(requestUrl, authorModeOn);
    
        // Assert
        IEnumerable<string> expected = new List<string>{"1 17/11/2022", "2 10/11/2022", "1 06/11/2022"};
        Assert.Equal(analysisResult,expected);
    }

}