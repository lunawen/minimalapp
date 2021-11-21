## How to run this project

Command Line: `dotnet run watch` - hot reload

Command Line: `dotnet run` 

Open solution in VS2022 and run the project.

## Packages

- Swagger for OpenAPI
- EntityFramework: InMemory, Sqlite and SqlServer all installed

### Change DbContext Option

**InMemory**

```csharp
builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("items"));

//.....

optionsBuilder.UseInMemoryDatabase("Todos");
```

**Sqlite**

Add connection string in `appsettings.Development.json` in this format:

```json
"ConnectionStrings": {
    "TodoDb": "Data Source=db\\TodoDb.db"
  },
```

Change code in Program.cs to Use Sqlite.

```csharp
var connectionString = builder.Configuration.GetConnectionString("TodoDb");
builder.Services.AddSqlite<TodoDb>(connectionString);

//....

optionsBuilder.UseSqlite();
```

**SqlServer**

Add connection string in `appsettings.Development.json` in this format:

```json
  "ConnectionStrings": {
    "TodoDb": "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=TodoDb; Integrated Security=True;"
  },
```

Change code in Program.cs to Use Sqlite.

```csharp
var connectionString = builder.Configuration.GetConnectionString("TodoDb");
builder.Services.AddSqlServer<TodoDb>(connectionString);

//....

optionsBuilder.UseSqlServer();
```

---

## EF migration

First you need to install the ef tool:

`dotnet tool install --global dotnet-ef`

Then you need to run db migration if you want to use sqlite or sqlserver.

`dotnet ef database update`

If for some reason it doesn't work, you can delete the files in Migrations and initiate a new db migration.

`dotnet ef migrations add initialcreate`

`dotnet ef database update`

---

## Blog post 

[ASP.NET Core Minimal API - Luna Tech (lunawen.com)](https://lunawen.com/dotnet/20211121-lunatech-aspnetcore-minimal-api/)

