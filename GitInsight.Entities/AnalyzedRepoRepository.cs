
using GitInsight.Entities.DTOs;

namespace GitInsight.Entities;

public class AnalyzedRepoRepository: IAnalyzedRepoRepository, IDisposable{

private GitInsightContext _context;
    public AnalyzedRepoRepository(GitInsightContext context){
        this._context = context;
    }

   public async Task<(Response,AnalyzedRepoDTO)> CreateAsync (AnalyzedRepoCreateDTO analyzedRepoDTO)
   {
    var analyzedRepo = await _context.AnalyzedRepos.FirstOrDefaultAsync(c => c.Path == analyzedRepoDTO.Path);
    Response response; 

    if(analyzedRepo == null){
            analyzedRepo = new AnalyzedRepo{
            State = analyzedRepoDTO.State,
            CommitsInRepo = analyzedRepoDTO.CommitsInRepo,
            Path = analyzedRepoDTO.Path
        };
        _context.AnalyzedRepos.Add(analyzedRepo);
        await _context.SaveChangesAsync();

        response = Response.Created;
    } else {
        response = Response.Conflict;
    }
        var created = new AnalyzedRepoDTO(analyzedRepo.Id, analyzedRepo.Path, analyzedRepo.State, analyzedRepo.CommitsInRepo);
        return (response, created);
    }

    public async Task<AnalyzedRepoDTO?> FindAsync(int analyzedRepoId)
    {
        var repo = from c in _context.AnalyzedRepos
                        let path = c.Path
                        where c.Id == analyzedRepoId
                        select new AnalyzedRepoDTO(c.Id,path,c.State,c.CommitsInRepo);

        return await repo.FirstOrDefaultAsync();
    }
    
    public async Task<Response> UpdateAsync(AnalyzedRepoUpdateDTO updateDTO){
       
        var repoInDB = await _context.AnalyzedRepos.FindAsync(updateDTO.Path);

         if (repoInDB == null)
        {
            return Response.NotFound;
        }

        var listSorted = updateDTO.CommitsInRepo.OrderBy(c => c.Date);

        var sortedRepoCommits = updateDTO.CommitsInRepo.Where(c => c.Date > repoInDB.State).Select(c => new DataCommit{
            StringId = c.StringId,
            Name = c.Name,
            Date = c.Date
        });

        if(sortedRepoCommits.ToList().Count()!=0){
            foreach(DataCommit dc in sortedRepoCommits){
                repoInDB.CommitsInRepo.Add(dc);
            }
            
            repoInDB.State = sortedRepoCommits.Last().Date;
        }
        _context.SaveChanges();
        return Response.Updated;
    }


    public void Dispose(){
        _context.Dispose();
    }

}