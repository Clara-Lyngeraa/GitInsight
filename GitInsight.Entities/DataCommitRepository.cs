using GitInsight.Entities.DTOs;

namespace GitInsight.Entities;


public class DataCommitRepository: IDataCommitRepository, IDisposable{

private GitInsightContext _context;
    public DataCommitRepository(GitInsightContext context){
        _context = context;
    }

    public async Task<(Response, string)> CreateAsync(DataCommitCreateDTO commit){

        var entity = await _context.DataCommits.FirstOrDefaultAsync(c => c.StringId == commit.StringId);
         Response response; 

        if(entity == null){
            var newCommit = new DataCommit{
            StringId = commit.StringId,
            Date = commit.Date,
            Name = commit.Name};

            _context.DataCommits.Add(newCommit);
            await _context.SaveChangesAsync();

            response = Response.Created;
        } else {
            //the commit is already there
            response = Response.Conflict;
        }

        var created = new DataCommitDTO(entity.StringId, entity.Name,entity.Date);
        
        return (response, created.StringId);
    }


    public async Task<DataCommitDTO?> FindAsync(string dataCommitId)
    {
         var dataCommit = from c in _context.DataCommits
                     where c.StringId == dataCommitId
                     select new DataCommitDTO(c.StringId, c.Name,c.Date);

        return await dataCommit.FirstOrDefaultAsync();
    }

    public async Task<Response> UpdateAsync(DataCommitUpdateDTO dataCommit)
    {
        var dataCommitInDB = await _context.DataCommits.FindAsync(dataCommit.StringId);
        Response response;

         if (dataCommitInDB == null)
        {
            response = Response.NotFound;
        }
        else
        {
            dataCommitInDB.Name = dataCommit.Name;
            dataCommitInDB.Date = dataCommit.Date;
            dataCommitInDB.StringId = dataCommit.StringId;
            await _context.SaveChangesAsync();
            response = Response.Updated;
        }

        return response;
    }

      public void Dispose(){
        _context.Dispose();
    }
}