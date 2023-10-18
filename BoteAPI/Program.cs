using BoteApi.Db;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Botes") ?? "Data Source=Botes.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<BoteDb>(connectionString);
builder.Services.AddSwaggerGen(bote =>
{
bote.SwaggerDoc("v1", new OpenApiInfo { Title = "Bote API", Description = "Keep track of your tasks", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(bote =>
{
    bote.SwaggerEndpoint("/swagger/v1/swagger.json", "Bote API V1");
});

app.MapGet("/", () => "Type '/swagger' at the end of the url");

app.MapGet("/botes", async (BoteDb db) => await db.Botes.ToListAsync());
app.MapPost("/botes", async (BoteDb db, Bote bote) => {
    await db.Botes.AddAsync(bote);
    await db.SaveChangesAsync();
    return Results.Created($"/bote/{bote.Id}", bote);
});
app.MapGet("/botes/{id}", async (BoteDb db, int id) => await db.Botes.FindAsync(id));
app.MapPut("/bote/{id}", async (BoteDb db, Bote updateBote, int id) =>
{
    var bote = await db.Botes.FindAsync(id);
    if (bote is null) return Results.NotFound();
    bote.Name = updateBote.Name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/botes/{id}", async (BoteDb db, int id) =>
{
   var bote = await db.Botes.FindAsync(id);
   if (bote is null)
   {
      return Results.NotFound();
   }
   db.Botes.Remove(bote);
   await db.SaveChangesAsync();
   return Results.Ok();
});

app.Run();

