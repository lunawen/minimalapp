using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("TodoDb");
builder.Services.AddSqlServer<TodoDb>(connectionString);

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

app.MapGet("/todos", async (TodoDb db) => await db.TodoItems.ToListAsync());

app.MapGet("/todos/{id:int}", async (TodoDb db, int id) => await db.TodoItems.FindAsync(id));

app.MapPut("/todos/{id:int}", async (TodoDb db, TodoItem updateTodo, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo == null) return Results.NotFound();

    todo.Item = updateTodo.Item;
    todo.Completed = updateTodo.Completed;

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.MapDelete("/todos/{id:int}", async (TodoDb db, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null) return Results.NotFound();
    db.TodoItems.Remove(todo);
    await db.SaveChangesAsync();

    return Results.Ok();

});


app.MapPost("/todos", async (TodoDb db, TodoItem todo) =>
{
    await db.TodoItems.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

// add razor page to render HTML
app.MapRazorPages();

app.Run();

internal class TodoItem
{
    public string Item { get; set; }
    public int Id { get; set; }
    public bool Completed { get; set; }

    public TodoItem(string Item)
    {
        this.Item = Item;
    }
}

class TodoDb: DbContext
{
    public TodoDb(DbContextOptions options): base(options) { }
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }
}