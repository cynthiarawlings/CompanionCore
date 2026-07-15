using CompanionCore.Api.Endpoints;

using CompanionCore.Core.Interfaces;
using CompanionCore.Infrastructure.Configuration;
using CompanionCore.Infrastructure.Services;
using CompanionCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanionCore.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpClient("Ollama", client =>
        {
            client.BaseAddress = new Uri("http://localhost:11434");
        });

        builder.Services.Configure<OllamaOptions>(
            builder.Configuration.GetSection("Ollama"));

        builder.Services.AddDbContext<CompanionDbContext>(options =>
            options.UseSqlite("Data Source=CompanionCore.db"));

        builder.Services.AddScoped<IChatService, OllamaChatService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapStatusEndpoints();
        app.MapChatEndpoints();

        app.Run();
    }
}
