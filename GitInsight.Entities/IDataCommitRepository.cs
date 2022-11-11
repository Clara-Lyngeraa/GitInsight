using GitInsight.Entities.DTOs;
namespace GitInsight.Entities;


public interface IDataCommitRepository
{
    Task<(Response, string)> CreateAsync(DataCommitCreateDTO dataCommit);
    Task<DataCommitDTO?> FindAsync(string dataCommitId);
    //Task<IReadOnlyCollection<CityDto>> ReadAsync();
    Task<Response> UpdateAsync(DataCommitUpdateDTO dataCommit);
    //Task<Status> DeleteAsync(int cityId);
}