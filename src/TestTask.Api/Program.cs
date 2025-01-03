using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using TestTask.Application;
using TestTask.Infrastructure;
using TestTask.Infrastructure.Exceptions;
using TestTask.Persistence.CalculationsDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddCors(
  options => options
    .AddPolicy("AngularPolicy",
  policyBuilder =>
  {
    policyBuilder
      .AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  }));

builder.Services.AddControllers(options => options.ModelValidatorProviders.Clear())
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomOperationIds(apiDescription =>
        apiDescription.TryGetMethodInfo(out var methodInfo)
            ? methodInfo.Name
            : null);
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddToDoListDb(builder.Configuration);

var app = builder.Build();


// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CalculationsDbContext>();

    context.Database.Migrate();
}

app.UseCustomExceptionHandler(app.Environment);

app.UseCors("AngularPolicy");
app.UseSwagger();
app.UseSwaggerUI(x => x.DisplayOperationId());
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
