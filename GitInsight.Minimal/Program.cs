using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GitInsightContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (GitInsightContext db) =>
    await db.DataCommits.ToListAsync());

// app.MapGet("/todoitems/complete", async (GitInsightContext db) =>
//     await db.DataCommits.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, GitInsightContext db) =>
    await db.DataCommits.FindAsync(id));
       
app.Run();
