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
//         var response = _repo.Create(createDTO);
//         var expected = Response.Created;

//         //Assert
//         response.Should().Be(expected);    
//     }

//     [Fact]
//     public void Create_creates_correct_AnalyzedRepo_with_DataCommits()
//     {
//         //Arrange
//         var commitsStrings = new List<string>();

//         foreach(Commit c in testRepo.Commits){
//             commitsStrings.Add(c.Id.ToString());
//         }

//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, commitsStrings);

//         //Act
//         _repo.Create(createDTO);
        
//         //Assert
//         Assert.Equal(4,_context.DataCommits.Count()); 
//     }



//     [Fact]
//     public void giveCommitsTehirDates_updates_dates_to_correct_dates()
//     {
//         //Arrange
//         var commitsStrings = new List<string>();

//             foreach(Commit c in testRepo.Commits){
//                 commitsStrings.Add(c.Id.ToString());
//             }

//         var createDTO = new AnalyzedRepoCreateDTO("hejsa", testRepo.Commits.Last().Author.When.Date, commitsStrings);
//          _repo.Create(createDTO);
//         var repoInDb = _context.AnalyzedRepos.Find("hejsa");
    
//         //Act
//         _repo.giveCommitsTheirDatesAndNames(testRepo);

//         //Assert
//         foreach(DataCommit dc in repoInDb.CommitsInRepo){
//             dc.Date.Should().Be(testRepo.Commits.Where(c => c.Id.ToString() == dc.StringId).First().Author.When.Date);
//         }
//     }

//     [Fact]
//     public void Update_updates_list_of_datacommits_in_db()
//     {
//         //Arrange
//         var createDTO = new AnalyzedRepoCreateDTO(AnalyzedRepoRepository.getRepoHashedID(testRepo), new DateTime(2022,09,10), new List<string>());
//          _repo.Create(createDTO);

//         var repoInDb = _context.AnalyzedRepos.Find(AnalyzedRepoRepository.getRepoHashedID(testRepo));
    
//         //Act
//         _repo.Update(testRepo);

        
//         // Act
//         var expected = 4;
    
//         // Assert
//         _context.AnalyzedRepos.Find(AnalyzedRepoRepository.getRepoHashedID(testRepo)).CommitsInRepo.Count().Should().Be(expected);
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
//         var repsonse = _repo.Create(createDTO);

//         //Assert
//         AnalyzedRepo actualFoundRepo = _repo.FindWithStringId("hejsa");
//         actualFoundRepo.RepositoryIdString.Should().Be(createDTO.RepositoryIdString);
//     }

//     [Fact]
//     public void findCommitsInRepo_return_correct_list_of_datacommits()
//     {
//        //Arrange
//         var expected = 4;
//        //Act
//        var actual = _repo.findCommitsInRepo(testRepo).Count();
//        //Assert
//        expected.Should().Be(actual);
        
//     }
// }

 






 




