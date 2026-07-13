using CompanionCore.Api.Endpoints;

using CompanionCore.Core.Interfaces;
using CompanionCore.Infrastructure.Services;

namespace CompanionCore.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

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
