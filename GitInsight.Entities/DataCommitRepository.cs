namespace GitInsight.Entities;

public class DataCommitRepository: IDisposable{

private GitInsightContext _context;
    public DataCommitRepository(GitInsightContext context){
        _context = context;
    }

    //creating a SignatureUpdateDTO from a signature
    private CommitUpdateDTO SignatureUpdateDTOFromSignature(DataCommit dataCommit){
        return new CommitUpdateDTO (
        repoId: dataCommit.RepositoryId!,
        name: dataCommit.Name!,
        email: dataCommit.Email!,
        date: dataCommit.Date
    );
    }
    
    // public Response Update(CommitSignatureUpdateDTO sign){

    //     //find the date of the mathcing signature
    //     //var toUpdate = _context.Signatures.Find(sign.date);
    //     var entity = _context.Signatures.Find(sign.repoId);
    //     Response response;

    //     if(entity is null)
    //     {
    //         response = Response.NotFound;
    //     } else 
    //     {
    //         entity.Name = sign.name;
    //         entity.Date = sign.date;
    //         _context.SaveChanges();
    //         response = Response.Updated;
    //     }
       
    //     return response;
    // }



    public Response Update(CommitUpdateDTO commit){
        
        return Response.NotFound;
    }

    public (Response response, int id) Create(CommitCreateDTO commit){
        
        return (Response.NotFound, 0);
    }



    public void Dispose(){
        _context.Dispose();
    }
}