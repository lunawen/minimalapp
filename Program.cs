var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// add another route
app.MapGet("/todos", () => new { TodoItem = "Learn about routing", Completed = false });

// route variables
app.MapGet("/hello/{name}", (string name) => $"Hi, {name}");

// specify variable type - add constraint
// used for disambiguate similar routes, not for input validation
// returns 404 if not match the type
app.MapGet("/todos/{id:int}", (int id) => $"Todo id: {id}");
app.MapGet("/todos/{active:bool}", (bool active) => $"Todo is active: {active}");
app.MapGet("/todos/{name}", (string name) => $"Todo name: {name}");

app.Run();
