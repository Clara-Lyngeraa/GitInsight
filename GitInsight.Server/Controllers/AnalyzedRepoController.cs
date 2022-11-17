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

  public AnalyzedRepoController(IAnalyzedRepoRepository repo)
  {
    _repository = repo;
    
  }

[HttpGet("{url}")]
  public async Task<IEnumerable<string>> Get(string url) 
  {
    Console.WriteLine("Got the url: " + url);
    var urlToUse = WebUtility.UrlDecode(url);
    var repo = new Repository(Repository.Clone(urlToUse,"ClonedRepo"));
    var createDTO = new AnalyzedRepoCreateDTO(repo, url);
   
    var (response, dto) = await _repository.CreateAsync(createDTO);

    var currentAnalyzedRepo = await _repository.FindAsync(dto.Id);

    if(currentAnalyzedRepo.State != repo.Commits.Last().Author.When){
      _repository.UpdateAsync(new AnalyzedRepoUpdateDTO(repo,url));

      currentAnalyzedRepo = await _repository.FindAsync(dto.Id);
      Console.WriteLine("trying to find repo with id: " + dto.Id + " in the database to update");
    }

    var commitAnalyzer = new CommitAnalyzer();
  
    var resultOfAnalysis = commitAnalyzer.getFrequency(currentAnalyzedRepo.commitsInRepo);

    Directory.Delete("/Users/monicahardt/Desktop/GitInsight/GitInsight.Server/ClonedRepo",true);
    return resultOfAnalysis;
  }
}

