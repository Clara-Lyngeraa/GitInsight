namespace GitInsight.Entities;

public class CommitSignatureRepository: IDisposable{

private GitInsightContext _context;
    public CommitSignatureRepository(GitInsightContext context){
        _context = context;
    }

    //creating a SignatureUpdateDTO from a signature
    private static CommitUpdateDTO SignatureUpdateDTOFromSignature(CommmitSignature sign){
        return new CommitUpdateDTO (
        repoId: sign.RepositoryId,
        name: sign.Name!,
        email: sign.Email!,
        date: sign.Date
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


    public void Dispose(){
        _context.Dispose();
    }
}