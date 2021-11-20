var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// add another route
app.MapGet("/todos", () => new { TodoItem = "Learn about routing", Completed = false });

// route variables
app.MapGet("/hello/{name}", (string name) => $"Hi, {name}");

app.Run();
