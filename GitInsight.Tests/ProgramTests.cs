namespace GitInsight.Entities;
using GitInsight;
using LibGit2Sharp;

public class ProgramTests: IDisposable
{

    private GitInsightContext _context;
    private AnalyzedRepoRepository _repo;
    private Repository testRepo;

    //whenever a test is run the constructor creates a CommitSignatureRepoTests object and a in-memory database"

    public ProgramTests(){
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
        testRepo.Dispose();
        Directory.Delete("./.git",true);
    }


   [Fact]
   public void getFrequency_returns_correct_db_data()
   {

    // Arrange
        var commitsStrings = new List<string>();
        foreach(var Commit in testRepo.Commits){
            commitsStrings.Add(Commit.Id.ToString());
        }

        var hashedId = Program.getRepoHashedID(testRepo);

        var createDTO = new AnalyzedRepoCreateDTO(hashedId,testRepo.Commits.Last().Author.When.Date, commitsStrings);
        var (response,id) = _repo.Create(createDTO);

        var analyzedRepo = _context.AnalyzedRepos.Find(id);
        IEnumerable<string> expected = new List<string>{"1 25/11/2022", "3 25/10/2022"};
    
        // Act
        var actual = Program.getFrequency(testRepo,_context);
    
        // Assert
        expected.Should().BeEquivalentTo(actual);
   }

   [Fact]
   public void getFrequencyauthor_returns_correct_db_data()
   {

    // Arrange
        var commitsStrings = new List<string>();
        foreach(var Commit in testRepo.Commits){
            commitsStrings.Add(Commit.Id.ToString());
        }

        var hashedId = Program.getRepoHashedID(testRepo);

        var createDTO = new AnalyzedRepoCreateDTO(hashedId,testRepo.Commits.Last().Author.When.Date, commitsStrings);
        var (response,id) = _repo.Create(createDTO);

        var analyzedRepo = _context.AnalyzedRepos.Find(id);
        IEnumerable<string> expected = new List<string>{"Person3",  "1 25/11/2022", "1 25/10/2022", "Person1", "1 25/10/2022", "Person2", "1 25/10/2022"};
    
        // Act
        var actual = Program.getFrequencyAuthorMode(testRepo,_context);
    
        // Assert
        expected.Should().BeEquivalentTo(actual);
   }
}

 




