// namespace GitInsight.Entities;

// public class AnalyzedRepoTests: IDisposable
// {

//     private GitInsightContext _context;
//     private AnalyzedRepoRepository _repo;

//     private Repository testRepo;

//     //whenever a test is run the constructor creates a CommitSignatureRepoTests object and a in-memory database"

//     public AnalyzedRepoTests(){
//         var connection = new SqliteConnection("Filename=:memory:");
//         connection.Open();
//         var builder = new DbContextOptionsBuilder<GitInsightContext>();
//         builder.UseSqlite(connection);

//         var context = new GitInsightContext(builder.Options);
//         context.Database.EnsureCreated();
//         _context = context;
//         _repo = new AnalyzedRepoRepository(_context);

//         //creates repository through libGit2Sharp
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
       
//     }
//     public void Dispose()   
//     {
//         _context.Dispose();
//         _repo.Dispose();
//         testRepo.Dispose();
//         Directory.Delete("./.git",true);
//     }


//     [Fact]
//     public void create_returns_response_created()
//     {
//         //Arrange
//         var commitsStrings = new List<string>();

//         foreach(Commit c in testRepo.Commits){
//             commitsStrings.Add(c.Id.ToString());
//         }

//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, commitsStrings);

//         //Act
//         var (response, id) = _repo.Create(createDTO);
//         var expected = Response.Created;

//         //Assert
//         response.Should().Be(expected);    
//         //id.Should().Be(1);    //since it is the first in the database the given id should be 1
//     }

     

//     [Fact]
//     public void Update_with_repo_not_in_db_returns_notfound_response()
//     {
//         // Arrange
//         var updateDTO = new AnalyzedRepoUpdateDTO("SomethingNotInDB", new DateTime(2022,11,06), new List<string>());
    
//         // Act
//         var expected = Response.NotFound;
//         var response = _repo.Update(updateDTO);
    
//         // Assert
//         response.Should().Be(expected);
//     }

//     [Fact]
//     public void Update_with_repo_in_db_with_no_changes_returns_updated_response()
//     {
//         // Arrange
//         //adding a repo to the db to test
//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", new DateTime(2022,11,06), new List<string>());

//         //Act
//         _repo.Create(createDTO);
//         var updateDTO = new AnalyzedRepoUpdateDTO("hejsa", new DateTime(2022,11,06), new List<string>());
    
//         // Act
//         var expected = Response.UpToDate;
//         var response = _repo.Update(updateDTO);
    
//         // Assert
//         response.Should().Be(expected);
//     }


//        [Fact]
//     public void find_with_stringId_should_work()
//     {
//         //Arrange
//         var commitsStrings = new List<string>();

//         foreach(Commit c in testRepo.Commits){
//             commitsStrings.Add(c.Id.ToString());
//         }

//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, commitsStrings);

//         //Act
//         var (repsonse,id) = _repo.Create(createDTO);

//         //Assert
//         var repoToFindId = _context.AnalyzedRepos.Find("hejsa").RepositoryIdString;
//         id.Should().Be(repoToFindId);
//     }

//         [Fact]
//     public void find_with_stringId_should_work_new()
//     {
//         //Arrange
//         var commitsStrings = new List<string>();

//         foreach(Commit c in testRepo.Commits){
//             commitsStrings.Add(c.Id.ToString());
//         }

//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, commitsStrings);

//         //Act
//         var (repsonse,id) = _repo.Create(createDTO);

//         //Assert
//         AnalyzedRepo actualFoundRepo = _repo.FindWithStringId("hejsa");
//         actualFoundRepo.RepositoryIdString.Should().Be(createDTO.RepositoryIdString);
//     }
  

// }

 




