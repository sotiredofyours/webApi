using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    var projectDicrectory = AppContext.BaseDirectory;
    var projectName = Assembly.GetExecutingAssembly().GetName().Name;
    var xmlFileName = $"{projectName}.xml";
    
    options.IncludeXmlComments(Path.Combine(projectDicrectory, xmlFileName));
});
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();