using Api.Controllers;
using Api.Models;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Api",
        Version = "v1",
        Description = "Это API позволяет работать с заданиями, позволяет удалять их, изменять, получать.",
        Contact = new OpenApiContact
        {
            Name = "Матвей",
            Email = "motya@mail.ru",
            Url = new Uri("https://vk.com/id1")
        },
        License = new OpenApiLicense
        {
            Name = "Лицензия GNU General Public License v3.0",
            Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html")
        }
    });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { Message = "API: OK" }))
   .WithName("HealthCheck")
   .WithTags("Health")
   .Produces(StatusCodes.Status200OK)
   .WithOpenApi(operation =>
   {
       operation.Summary = "Проверка состояния API";
       operation.Description = "Возвращает статус работоспособности сервиса.";
       return operation;
   });

app.MapGet("/tasks/search", (HttpRequest http) =>
{
    if (!http.Query.TryGetValue("from", out var fromVals) || !http.Query.TryGetValue("to", out var toVals))
        return Results.BadRequest(new { error = "Параметры 'from' и 'to' обязательны в формате yyyy-MM-dd" });

    if (!DateOnly.TryParse(fromVals.First(), out var fromDate) || !DateOnly.TryParse(toVals.First(), out var toDate))
        return Results.BadRequest(new { error = "Неверный формат даты. Ожидается yyyy-MM-dd" });

    if (fromDate > toDate)
    {
        var tmp = fromDate;
        fromDate = toDate;
        toDate = tmp;
    }

    var result = TaskController.Tasks.Where(t => t.EndOfTask >= fromDate && t.EndOfTask <= toDate).ToList();

    return Results.Ok(result);
})
.WithName("SearchTasksByDateRange")
.WithTags("Tasks")
.Produces<IEnumerable<MyTask>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi(operation =>
{
    operation.Summary = "Поиск задач по диапазону дат";
    operation.Description = "Возвращает задачи, у которых EndOfTask попадает в указанный диапазон (включительно). Параметры query: from, to (формат yyyy-MM-dd).";
    operation.Parameters.Add(new OpenApiParameter
    {
        Name = "from",
        Description = "Начальная дата диапазона (yyyy-MM-dd)",
        In = Microsoft.OpenApi.Models.ParameterLocation.Query
    });
    operation.Parameters.Add(new OpenApiParameter
    {
        Name = "to",
        Description = "Конечная дата диапазона (yyyy-MM-dd)",
        In = ParameterLocation.Query
    });
    return operation;
});

app.Run();
