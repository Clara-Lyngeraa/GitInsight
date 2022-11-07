namespace GitInsight.Entities;

public class DataCommitRepository: IDisposable{

private GitInsightContext _context;
    public DataCommitRepository(GitInsightContext context){
        _context = context;
    }

    //creating a SignatureUpdateDTO from a signature
    private CommitUpdateDTO SignatureUpdateDTOFromSignature(DataCommit dataCommit){
        return new CommitUpdateDTO (
        StringId: dataCommit.StringId,
        Name: dataCommit.Name!,
        Date: dataCommit.Date
    );
    }
    
    public (Response response, string id) Create(CommitCreateDTO commit){

    //finding the commitSignatures in the database that belongs to those in the analyzes repos list of names of the commits
        var newCommit = new DataCommit{
        StringId = commit.StringId,
        Date = commit.Date,
        Name = commit.Name};
    
        _context.DataCommits.Add(newCommit);
        _context.SaveChanges();
        return (Response.Created, newCommit.StringId);
    }

    public void Dispose(){
        _context.Dispose();
    }
}