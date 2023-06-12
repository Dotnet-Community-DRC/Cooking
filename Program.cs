var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/recipes", () =>
{
    // list of recipes
    var recipes = new List<string>
    {
        "Patate Douce",
        "Bugali na Sombe",
        "Sambaza an chips"
    };

    return recipes;
});

app.Run();
