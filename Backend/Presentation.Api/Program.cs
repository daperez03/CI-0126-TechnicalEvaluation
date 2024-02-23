using TechnicalEvaluation.Application;
using TechnicalEvaluation.Infrastructure;
using TechnicalEvaluation.Presentation.Api.Careers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Method registering services related to the application layer (business logic, use cases).
builder.Services.AddApplicationLayerServices();

// Method registering services related to the infrastructure layer (data access).
builder.Services.AddInfrastructureLayerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");
app.UseStaticFiles();

app.UseHttpsRedirection();

// Register the routes to the endpoints and their handlers.
app.RegisterCareersEndpoints();

app.Run();
