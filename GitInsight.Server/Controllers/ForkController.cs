using Microsoft.Extensions.Configuration;
using Extensions.HttpExtensions.Http;

namespace GitInsight.Server.Controllers;

public class ForkController
{

    private readonly HttpClient _client;

    public ForkController()
        //Skal lave den der token for det fungere, tror det er sådan her man gør
        //Skal nok have impoteret nogle pakker, men vs fucker lidt med farver nu
    {
        var config = new ConfigurationBuilder().AddUserSecrets<ForkController>().Build();
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://api.github.com");
        var token = config["AuthenticationTokens:GitHubAPI"];


        _client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
    }

    public IEnummerable<Fork> getForkList()
    {
        //Tænke denne metode skulle bruges til at få en liste af forks vi kunne bruge i forks.razor
        var forks = new List<>();
        return forks;
    }




}
//     [HttpGet("{url}")]
//   public async Task<IEnumerable<string>> Get(string url) 
//   {
//     Console.WriteLine("Got the url: " + url);
//     var urlToUse = WebUtility.UrlDecode(url);
//     var repo = new Repository(Repository.Clone(urlToUse,"ClonedRepo"));
 
//     var commitAnalyzer = new CommitAnalyzer();
//     var forkList = commitAnalyzer.GetForkList(currentAnalyzedRepo!.commitsInRepo);
    
//     return forkList;
//   }


  
