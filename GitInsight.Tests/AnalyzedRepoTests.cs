// namespace GitInsight.Tests;

// using GitInsight;
// using LibGit2Sharp;
// using GitInsight.Core;
// using GitInsight.Entities;


// [Collection("Sequential")]
// public class AnalyzedRepoTests: IDisposable
// {
//     private Repository testRepo;

//     private readonly GitInsightContext _context;
//     private readonly AnalyzedRepoRepository _repository;


//     //whenever a test is run the constructor creates a CommitSignatureRepoTests object and a in-memory database"

//     public AnalyzedRepoTests(){
//         var connection = new SqliteConnection("Filename=:memory:");
//         connection.Open();
//         var builder = new DbContextOptionsBuilder<GitInsightContext>();
//         builder.UseSqlite(connection);

//         var context = new GitInsightContext(builder.Options);
//         context.Database.EnsureCreated();
//         _context = context;
//         _repository = new AnalyzedRepoRepository(_context);

//          //creates repository through libGit2Sharp
//         var path = Repository.Init(".");

//          //creates a repository object from the path above
//          testRepo = new Repository(path);

// //         //Creates 3 commits to the same date
//          Signature sig1 = new Signature("Person1", "person1@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
//          Signature sig2 = new Signature("Person2", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
//          Signature sig3 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,10,25)));
//          Signature sig4 = new Signature("Person3", "person2@itu.dk", new DateTimeOffset(new DateTime(2022,11,25)));

//          testRepo.Commit("Inital commit", sig1, sig1, new CommitOptions() {AllowEmptyCommit = true});
//          testRepo.Commit("Inital commit1", sig2, sig2, new CommitOptions() {AllowEmptyCommit = true});
//          testRepo.Commit("Inital commit2", sig3, sig3, new CommitOptions() {AllowEmptyCommit = true});
//          testRepo.Commit("Inital commit3", sig4, sig4, new CommitOptions() {AllowEmptyCommit = true});
        
//          //Console.WriteLine(repo.Commits.First().Message);
//     }
//     public void Dispose()
//     {
//         _context.Dispose();
//         testRepo.Dispose();
//         Directory.Delete("./.git",true);
//     }


//     [Fact]
//     public void create_returns_response_created()
//     {
//         //Arrange
//         var expectedDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, new List<string>());

//         //Act
//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, new List<string>());
//         var (response, id) = _repository.Create(createDTO);

//         //Assert
//         response.Should().Be(Response.Created);        
       
//     }
// }


namespace GitInsight.Entities;

public class AnalyzedRepoTests: IDisposable
{

    private GitInsightContext _context;
    private AnalyzedRepoRepository _repo;

    private Repository testRepo;

    //whenever a test is run the constructor creates a CommitSignatureRepoTests object and a in-memory database"

    public AnalyzedRepoTests(){
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitInsightContext>();
        builder.UseSqlite(connection);

        var context = new GitInsightContext(builder.Options);
        context.Database.EnsureCreated();
        _context = context;
        _repo = new AnalyzedRepoRepository(_context);

        //creates repository through libGit2Sharp
        var path = Repository.Init(".");

         //creates a repository object from the path above
         testRepo = new Repository(path);

//         //Creates 3 commits to the same date
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
        //testRepo.Dispose();
        //Directory.Delete("./.git",true);
    }


    [Fact]
    public void create_returns_response_created()
    {
        //Arrange
        var expectedDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, new List<string>());

        //Act
        var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, new List<string>());
        var (response, id) = _repo.Create(createDTO);

        //Assert
        response.Should().Be(Response.Created);        
       
    }

}
// }
 




