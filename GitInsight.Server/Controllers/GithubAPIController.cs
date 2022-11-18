using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace GitInsight.Server.Controllers;


[ApiController]
[Route("[controller]")]

public class GithubAPIController: ControllerBase{

    

    public GithubAPIController(){

        var client = new GitHubClient(new ProductHeaderValue("hej"));
        var tokenAuth = new Credentials("PAT_Token");
        client.Credentials = tokenAuth;
        var data = client.Repository.GetAllForCurrent();
        



        // client = new HttpClient();
        // client.BaseAddress = new Uri("https://api.github.com");
        // var token = "<token>";

        // client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
        // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

        

        //https://gist.github.com/MaximRouiller/74ae40aa994579393f52747e78f26441

    }


    //https://docs.github.com/en/rest/repos/forks#list-forks
    [HttpGet]
    public async Task<IEnumerable<string>> getForks(string owner, string repo){

        //var response = await client.GetAsync($"/repos/{owner}/{repo}/forks");
        return new List<string>{"hej"};
    }
}