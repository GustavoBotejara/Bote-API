using BoteApi.DB;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
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

app.MapGet("/botes", () => BoteDB.GetBotes());
app.MapGet("/botes/{id}", (int id) => BoteDB.GetBote(id));
app.MapPost("/botes", (Bote bote) => BoteDB.CreateBote(bote));
app.MapPut("/botes", (Bote bote) => BoteDB.UpdateBote(bote));
app.MapDelete("/botes/{id}", (int id) => BoteDB.DeleteBote(id));

app.Run();

