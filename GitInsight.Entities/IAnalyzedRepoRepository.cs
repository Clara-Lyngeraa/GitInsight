
namespace GitInsight.Entities;
using GitInsight.Entities.DTOs;

public interface IAnalyzedRepoRepository
{
    Task<(Response,AnalyzedRepoDTO)> CreateAsync(AnalyzedRepoCreateDTO analyzedRepo);
    //Task<CharacterDetailsDto?> FindAsync(int characterId);
    //Task<IReadOnlyCollection<CharacterDto>> ReadAsync();
    Task<Response> UpdateAsync(AnalyzedRepoUpdateDTO analyzedRepo);
    //Task<Status> DeleteAsync(int characterId);
}