using GitInsight.Entities;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GitInsight.Server.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<GitInsightContext>();
builder.Services.AddScoped<IAnalyzedRepoRepository, AnalyzedRepoRepository>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    

    
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
