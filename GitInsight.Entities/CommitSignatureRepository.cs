namespace GitInsight.Entities;

public class CommitSignatureRepository: ICommitSignatureRepository, IDisposable{

private GitInsightContext _context;
    public CommitSignatureRepository(GitInsightContext context){
        _context = context;
    }

    //creating a SignatureUpdateDTO from a signature
    private static CommitSignatureUpdateDTO SignatureUpdateDTOFromSignature(CommmitSignature sign){
        return new CommitSignatureUpdateDTO (
        repoId: sign.RepositoryId,
        name: sign.Name!,
        email: sign.Email!,
        date: sign.Date
    );
    }
    
    public Response Update(CommitSignatureUpdateDTO sign){

        //find the date of the mathcing signature
        //var toUpdate = _context.Signatures.Find(sign.date);
        var entity = _context.Signatures.Find(sign.repoId);
        Response response;

        if(entity is null)
        {
            response = Response.NotFound;
        } else 
        {
            entity.Name = sign.name;
            entity.Date = sign.date;
            _context.SaveChanges();
            response = Response.Updated;
        }
       
        return response;
    }
    public void Dispose(){
        _context.Dispose();
    }
}