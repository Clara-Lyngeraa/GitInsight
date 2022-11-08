public class CommitAnalyzerTests
{

    private CommitAnalyzer commitAnalyzer;

    //whenever a test is run the constructor creates a CommitSignatureRepoTests object and a in-memory database"

    public CommitAnalyzerTests(){
        commitAnalyzer = new CommitAnalyzer();
    }

   [Fact]
   public void getFrequency_returns_correct_db_data()
   {

        //Arrange
        var commitsToAnalyze = new List<DataCommit> {};
        DataCommit dc1 = new DataCommit{StringId = "01", Name = "Person1", Date = new DateTime(2022,10,25)};
        DataCommit dc2 = new DataCommit{StringId = "02", Name = "Person1", Date = new DateTime(2022,10,25)};
        DataCommit dc3 = new DataCommit{StringId = "03", Name = "Person2", Date = new DateTime(2022,11,25)};

        commitsToAnalyze.Add(dc1);
        commitsToAnalyze.Add(dc2);
        commitsToAnalyze.Add(dc3);

        IEnumerable<string> expected = new List<string>{"2 25/10/2022", "1 25/11/2022"};

        // Act
        var actual = commitAnalyzer.getFrequency(commitsToAnalyze);
    
        // Assert
        expected.Should().BeEquivalentTo(actual);
   }

   [Fact]
   public void getFrequencyauthor_returns_correct_db_data()
   {

        //Arrange
        var commitsToAnalyze = new List<DataCommit> {};
        DataCommit dc1 = new DataCommit{StringId = "01", Name = "Person1", Date = new DateTime(2022,10,25)};
        DataCommit dc2 = new DataCommit{StringId = "02", Name = "Person1", Date = new DateTime(2022,10,25)};
        DataCommit dc3 = new DataCommit{StringId = "03", Name = "Person2", Date = new DateTime(2022,11,25)};

        commitsToAnalyze.Add(dc1);
        commitsToAnalyze.Add(dc2);
        commitsToAnalyze.Add(dc3);


        //maybe it would be nicde without the commas?
        IEnumerable<string> expected = new List<string>{"Person1", "2 25/10/2022", "Person2", "1 25/11/2022"};

        // Act
        var actual = commitAnalyzer.getFrequencyAuthorMode(commitsToAnalyze);
    
        // Assert
        expected.Should().BeEquivalentTo(actual);
   }
}
