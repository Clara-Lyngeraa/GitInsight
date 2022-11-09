namespace GitInsight.Entities;
using GitInsight.Entities;

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
        testRepo.Dispose();
        Directory.Delete("./.git",true);
    }


//     [Fact]
//     public void create_returns_response_created()
//     {       
//         //Arrange
//         var createDTO = new AnalyzedRepoCreateDTO(testRepo);

//         //Act
//         var response = _repo.Create(createDTO);
//         var expected = Response.Created;

//         //Assert
//         response.Should().Be(expected);    
//         Assert.Equal(_context.AnalyzedRepos.Count(),1);
//         //Assert.Equal(_context.AnalyzedRepos.Find(AnalyzedRepoRepository.getRepoHashedID(testRepo)).State.ToShortDateString().ToString(),testRepo.Commits.Last().Author.When.Date.ToShortDateString().ToString());
        
//     }



//     [Fact]
//     public void Create_creates_correct_AnalyzedRepo_with_DataCommits()
//     {
//         //Arrange
//         var createDTO = new AnalyzedRepoCreateDTO(testRepo);

//         //Act
//         _repo.Create(createDTO);
        
//         //Assert
//         Assert.Equal(4,_context.DataCommits.Count()); 
//     }

//     [Fact]
//     public void check_if_datacommits_are_updated_in_datacommit_database_on_create()
//     {
//         //Arrange
//         var createDTO = new AnalyzedRepoCreateDTO(testRepo);
//         _repo.Create(createDTO);

//         //Act
//         var dbRepo = _context.AnalyzedRepos.Find(AnalyzedRepoRepository.getRepoHashedID(testRepo));

//         var expectedListOfDatacommits = new List<DataCommit>();
//         foreach(Commit c in testRepo.Commits){
//             expectedListOfDatacommits.Add(new DataCommit{
//             StringId = c.Id.ToString(),
//             Name = c.Author.Name,
//             Date = c.Author.When.Date,
//             Repo = dbRepo
//             });
//         }
//         //Assert
//         _context.DataCommits.Count().Should().Be(expectedListOfDatacommits.Count()); 
// }

        [Fact]
        public void finding_a_repo_is_db_should_return_correct_analyzedRepo()
        {
            //Arrange
            var createDTO = new AnalyzedRepoCreateDTO(testRepo);
            AnalyzedRepo dbRepoFromCreate = _repo.Create(createDTO);
            Console.WriteLine(dbRepoFromCreate.Path);

            //Act
            var dbRepo = _context.AnalyzedRepos.Find(testRepo.Info.Path);
            Console.WriteLine(dbRepo.Path);


            //Assert
            Assert.Equal(dbRepoFromCreate,dbRepo);
        }


         [Fact]
        public void repo_path_with_new_commit_in_repo_should_be_the_same()
        {
            //Arrange
            var createDTO = new AnalyzedRepoCreateDTO(testRepo);
            AnalyzedRepo dbRepoFromCreate = _repo.Create(createDTO);

            Signature sig5 = new Signature("Person4", "person5@itu.dk", new DateTimeOffset(new DateTime(2022,11,30)));
            testRepo.Commit("new Commit", sig5,sig5, new CommitOptions (){AllowEmptyCommit = true});

            //Act
            var dbRepo = _context.AnalyzedRepos.Find(testRepo.Info.Path);

            //Assert
            Assert.Equal(dbRepoFromCreate,dbRepo);
        }


    [Fact]
    public void Update_updates_list_of_datacommits_in_db()
    {
        //Arrange
        var createDTO = new AnalyzedRepoCreateDTO(testRepo);
         _repo.Create(createDTO);

        //giving testrepo a new commit
        Signature sig5 = new Signature("Person4", "person5@itu.dk", new DateTimeOffset(new DateTime(2022,11,30)));
        testRepo.Commit("new Commit", sig5,sig5, new CommitOptions (){AllowEmptyCommit = true});
       
        //Act
        _repo.Update(new AnalyzedRepoUpdateDTO(testRepo));
        
        // Act
        var expected = 5;
    
        // Assert 
        //_context.DataCommits.Count().Should().Be(expected);
        Assert.Equal(_context.AnalyzedRepos.Find(testRepo.Info.Path).CommitsInRepo.Count(),5);
        }


    [Fact]
    public void Update_updates_list_of_datacommits_in_db_to_correctlist()
    {
        //Arrange
        var createDTO = new AnalyzedRepoCreateDTO(testRepo);
         _repo.Create(createDTO);

        //giving testrepo a new commit
        Signature sig5 = new Signature("Person4", "person5@itu.dk", new DateTimeOffset(new DateTime(2022,11,30)));
        testRepo.Commit("new Commit", sig5,sig5, new CommitOptions (){AllowEmptyCommit = true});

        Signature sig6 = new Signature("Person4", "person5@itu.dk", new DateTimeOffset(new DateTime(2022,11,30)));
        testRepo.Commit("new Commit", sig6,sig6, new CommitOptions (){AllowEmptyCommit = true});
       
        //Act
        _repo.Update(new AnalyzedRepoUpdateDTO(testRepo));
        
    
        // Assert 
        Assert.Equal(_context.AnalyzedRepos.Find(testRepo.Info.Path).CommitsInRepo.Count(),6);
        }

    
        [Fact]
        public void FindCommitsInRepo_returns_correct_list_of_datacommits_with_firsttime_repo()
        {
            //Assert
           


            //Act
            var actualList = _repo.findCommitsInRepo(testRepo);



            //Arrange
            actualList.Count().Should().Be(4);
        }


  
    }





 






 




