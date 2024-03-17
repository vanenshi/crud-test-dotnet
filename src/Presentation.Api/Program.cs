using Application;
using Infrastructure;
using Presentation.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration).AddApplication().AddPresentation(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePresentation(configuration);
app.MapControllers();

app.Run();

public partial class Program { }
