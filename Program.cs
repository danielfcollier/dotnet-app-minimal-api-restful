using Model;
using Transaction;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/reset", async () =>
{
    await Db.Handler.Reset();
    Results.StatusCode(200);
    return Results.Text("OK");
});

app.MapGet("/balance", async (HttpRequest request) =>
{
    string id = request.Query["account_id"];

    Account? data = await Db.Handler.Read(id);

    if (data is null)
    {
        return Results.NotFound(0);
    }

    return Results.Ok(data.Balance);
});

app.MapPost("/event", (Event data) =>
{
    Operation result = new ()
    {
        Origin = new Source()
        {
            Id = "100",
            Balance = 15
        }
    };

    if (result is null)
    {
        return Results.NotFound(0);
    }

    return Results.Json(result, null, null, 201);
});

app.Run("http://localhost:4000");