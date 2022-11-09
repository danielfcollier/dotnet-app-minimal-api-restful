using Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/reset", async () =>
{
    await Db.Handler.Reset();
    return Results.Ok("OK");
});

app.MapGet("/balance", async (HttpRequest request) =>
{
string id = request.Query["account_id"];

Account? data = await Db.Handler.Read(id);

if (data is not null)
{
    return Results.Ok(data.Balance);
}

return Results.NotFound(0);
});

app.Run("http://localhost:4000");