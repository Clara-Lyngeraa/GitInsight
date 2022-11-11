using Microsoft.AspNetCore.Mvc;

namespace GitInsight.Server.Controllers;

[ApiController]
[Route("[controller]")]

public class AnalyzedRepoController: ControllerBase{

  private readonly IAnalyzedRepoRepository _repository;
   

}

