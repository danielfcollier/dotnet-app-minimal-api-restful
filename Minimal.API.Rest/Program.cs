using Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/reset", async Task<IResult> () =>
{
    await Db.Handler.Reset();
    Results.StatusCode(200);

    return Results.Text("OK");
});

app.MapGet("/balance", async Task<IResult> (HttpRequest request) =>
{
    string id = request.Query["account_id"];

    Account? data = await Db.Handler.Read(id);

    if (data is null)
    {
        return Results.NotFound(0);
    }

    return Results.Ok(data.Balance);
});

app.MapPost("/event", async Task<IResult> (Event data) =>
{
    try
    {
        Transaction result = await Operation.Bank.Handler(data);

        return Results.Json(result, null, null, 201);
    }
    catch
    {
        return Results.NotFound(0);
    }
});

if (app.Environment.EnvironmentName != "Test") {
    var port = Environment.GetEnvironmentVariable("PORT") ?? "4000";
    app.Urls.Add($"http://0.0.0.0:{port}");
} 
app.Run();

public partial class Program
{ }