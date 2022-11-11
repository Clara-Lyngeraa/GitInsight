// using Microsoft.AspNetCore.Mvc;
// using GitInsight.Entities;
// using GitInsight.Entities.DTOs;

// namespace GitInsight.Server.Controllers;

// [ApiController]
// [Route("[controller]")]

// public class DataCommitController: ControllerBase{

//   private readonly ILogger<DataCommitController> _logger;
//   private readonly IDataCommitRepository _repository;

//   public DataCommitController(ILogger<DataCommitController> logger, IDataCommitRepository repo)
//   {
//     _logger = logger;
//     _repository = repo;
//   }

//   [HttpGet]
//   public async Task<ActionResult<DataCommitDTO>> Get(string id) 
//   {
//     var newRepo = await _repository.FindAsync(id);

//     return 
//   }

// [HttpPost]
//   public async Task<IActionResult> Post(DataCommitCreateDTO DataCommit){

//         var created = await _repository.CreateAsync(DataCommit);

//         return

//   }
  
//    [HttpPut("{id}")]
//    public async Task<IActionResult> Put(string id, DataCommitUpdateDTO DataCommit){

//         var result = await _repository.UpdateAsync(DataCommit);

//         return

//    }

// }