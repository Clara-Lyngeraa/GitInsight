// using Microsoft.AspNetCore.Mvc;
// using GitInsight.Entities;
// using GitInsight.Entities.DTOs;
// using LibGit2Sharp;

// namespace GitInsight.Server.Controllers;

// [ApiController]
// [Route("[controller]")]

// public class AnalyzedRepoController: ControllerBase{

//   private readonly ILogger<AnalyzedRepoController> _logger;
//   private readonly IAnalyzedRepoRepository _repository;

//   public AnalyzedRepoController(ILogger<AnalyzedRepoController> logger, IAnalyzedRepoRepository repo)
//   {
//     _logger = logger;
//     _repository = repo;
//   }

//   [HttpGet("{url}")]
//   public async Task<IEnumerable<string>> Get(string url) 
//   {
//     var repo = new Repository(Repository.Clone(url,"ClonedRepo"));
    
//     var allCommitsToAnalyze = await findCommitsInRepoAsync(repo);

//     var commitAnalyzer = new CommitAnalyzer();
//     var resultOfAnalysis = commitAnalyzer.getFrequency(allCommitsToAnalyze);

//     return resultOfAnalysis;
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

