using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your todos", Version = "v1" });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});

if (app.Environment.IsDevelopment())
{
    app.MapGet("/OnlyInDev",
        () => "This can only be accessed when the app is running in development.");
}

if (app.Environment.EnvironmentName == "TestEnvironment")
{
    app.MapGet("/OnlyInTestEnvironment", () => "TestEnvironment");
}

//app.MapGet("/", () => "Hello World!");

// add another route
app.MapGet("/todos", () => new { TodoItem = "Learn about routing", Completed = false });

// route variables
app.MapGet("/hello/{name}", (string name) => $"Hi, {name}");

// another way to access route values
app.MapGet("/greetings/{name}", (HttpContext ctx) => $"Hello, {ctx.Request.RouteValues["name"]}");

// specify variable type - add constraint
// used for disambiguate similar routes, not for input validation
// returns 404 if not match the type
app.MapGet("/todos/{id:int}", (int id) => $"Todo id: {id}");
app.MapGet("/todos/{active:bool}", (bool active) => $"Todo is active: {active}");
app.MapGet("/todos/{name}", (string name) => $"Todo name: {name}");


// error handling
app.MapGet("/dobad", () => int.Parse("this is not an int"));

app.MapPost("/todos", (Todo todo) => todo.Name);

// add razor page to render HTML
app.MapRazorPages();

app.Run();

internal class Todo
{
    public string Name { get; set; }
    public int Id { get; set; }
    public bool Completed { get; set; }
}
