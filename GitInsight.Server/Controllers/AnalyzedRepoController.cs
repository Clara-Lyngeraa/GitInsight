using Microsoft.AspNetCore.Mvc;
using GitInsight.Entities;
using GitInsight.Entities.DTOs;
using LibGit2Sharp;
using System.Net;

namespace GitInsight.Server.Controllers;

[ApiController]
[Route("[controller]")]

public class AnalyzedRepoController: ControllerBase{

  //private readonly ILogger<AnalyzedRepoController> _logger;
  private readonly IAnalyzedRepoRepository _repository;

  private CommitAnalyzer commitAnalyzer;

  public AnalyzedRepoController(IAnalyzedRepoRepository repo)
  {
    _repository = repo;
    commitAnalyzer = new CommitAnalyzer();
  }

[HttpGet("{url}/{authorMode}")]
  public async Task<IEnumerable<string>> Get(string url, bool authorMode) 
  {
  
    var urlToUse = WebUtility.UrlDecode(url);
    var repo = new Repository(Repository.Clone(urlToUse,"ClonedRepo"));
    var createDTO = new AnalyzedRepoCreateDTO(repo, url);
   
    var (response, dto) = await _repository.CreateAsync(createDTO);
    var currentAnalyzedRepo = await _repository.FindAsync(dto.Id);

    if(response == global::Response.Conflict){
      //inserted repo was already in the db
    if(currentAnalyzedRepo!.State != repo.Commits.Last().Author.When){
      await _repository.UpdateAsync(new AnalyzedRepoUpdateDTO(repo,url));
      currentAnalyzedRepo = await _repository.FindAsync(dto.Id);
      }
    } 
    Directory.Delete("/Users/monicahardt/Desktop/GitInsight/GitInsight.Server/ClonedRepo",true);


    if(authorMode){
      var resultOfAnalysis = commitAnalyzer.getFrequencyAuthorMode(currentAnalyzedRepo!.commitsInRepo);
      return resultOfAnalysis;
    } else {
      var resultOfAnalysis = commitAnalyzer.getFrequency(currentAnalyzedRepo!.commitsInRepo);
      return resultOfAnalysis;
    }
  }
}

