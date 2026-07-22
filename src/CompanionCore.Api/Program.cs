using CompanionCore.Api.Endpoints;
using CompanionCore.Core.Interfaces;
using CompanionCore.Infrastructure.Configuration;
using CompanionCore.Infrastructure.Persistence;
using CompanionCore.Infrastructure.Services.AI;
using CompanionCore.Infrastructure.Services.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CompanionCore.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("React",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpClient("Ollama", (services, client) =>
        {
            var options = services
                .GetRequiredService<IOptions<OllamaOptions>>()
                .Value;

            client.BaseAddress = new Uri(options.BaseUrl);
        });

        builder.Services.Configure<OllamaOptions>(
            builder.Configuration.GetSection("Ollama"));

        builder.Services.AddDbContext<CompanionDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("CompanionCore")));

        builder.Services.AddScoped<IChatService, OllamaChatService>();
        builder.Services.AddScoped<IConversationService, ConversationService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("React");

        app.MapStatusEndpoints();
        app.MapChatEndpoints();
        app.MapConversationEndpoints();
        app.MapCompanionEndpoints();

        app.Run();
    }
}
