using Microsoft.AspNetCore.Mvc;
using GitInsight.Entities;
using GitInsight.Entities.DTOs;
using LibGit2Sharp;

namespace GitInsight.Server.Controllers;

[ApiController]
[Route("[controller]")]

public class AnalyzedRepoController: ControllerBase{

  private readonly ILogger<AnalyzedRepoController> _logger;
  private readonly IAnalyzedRepoRepository _repository;

  public AnalyzedRepoController(IAnalyzedRepoRepository repo)
  {
    _repository = repo;
    
  }

  [HttpGet("{id}")]
    public async Task<AnalyzedRepoDTO> Get(int id)
    {
        //creates repository through libGit2Sharp
        var path = Repository.Init(".");

         //creates a repository object from the path above
        Repository testRepo = new Repository(path);
        var (response, repoFound) =  await _repository.CreateAsync(new AnalyzedRepoCreateDTO(testRepo));

        var repo = await _repository.FindAsync(repoFound.Id);

        if (repo is null)
        {
            return new AnalyzedRepoDTO(0, "hejmeddig", new DateTime(2022,11,18),new List<DataCommit>());
        }
        return repo;
    }


//   [HttpGet("{url}")]
//   public async Task<string> Get(string url) 
//   {
//     return await Task.Run(() => url);

//     // var repo = new Repository(Repository.Clone(url,"ClonedRepo"));
    
//     // var allCommitsToAnalyze = await findCommitsInRepoAsync(repo);

//     // var commitAnalyzer = new CommitAnalyzer();
//     // var resultOfAnalysis = commitAnalyzer.getFrequency(allCommitsToAnalyze);

//     // return resultOfAnalysis;
//   }


//     //THIS METHOD SHOULD ONLY TAKE A REPO STRING
//     // WE HAVE A PROBLEM IF WE MAKE THE REPO IN THE PROGRAM. I DO NOT THINK THAT THIS IS POSSIBLE
//     // REQUIRES EXTERN AUTHENTICATION AND A CLONE
//     public async Task<IEnumerable<DataCommit>> findCommitsInRepoAsync(Repository repo){
//         //calling create 
//         //create has check if it is already in the db, if not it creates it
//         var createDTO = new AnalyzedRepoCreateDTO(repo);
//         var (response, dto) = await _repository.CreateAsync(createDTO);

//         var currentAnalyzedRepo = await _repository.FindAsync(dto.Id)!;

//         //check if the repo found in the database is up to date
//         if (currentAnalyzedRepo.State != getTimeOfLastestCommit(repo))
//         {
//             var updateDTO = new AnalyzedRepoUpdateDTO(repo);
//             //if it is not up to date we want to call update
//             await _repository.UpdateAsync(updateDTO);

//             //we find the updates version here
//             currentAnalyzedRepo = await _repository.FindAsync(dto.Id)!;
//         }
//         return currentAnalyzedRepo.commitsInRepo;
//     }

//    public DateTime getTimeOfLastestCommit(Repository repo){
//         return repo.Commits.Last().Author.When.Date;
//    }
// }
}

