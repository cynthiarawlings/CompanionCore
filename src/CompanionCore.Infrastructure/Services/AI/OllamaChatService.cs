using Microsoft.Extensions.Http;
using System.Net.Http.Json;
using CompanionCore.Core.Interfaces;
using CompanionCore.Core.Models;
using CompanionCore.Infrastructure.Models;
using CompanionCore.Infrastructure.Prompts;

namespace CompanionCore.Infrastructure.Services.AI;

public class OllamaChatService : IChatService
{
    private readonly HttpClient _httpClient;

    public OllamaChatService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Ollama");
    }

    public async Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        var ollamaRequest = new OllamaChatRequest
        {
            Model = "llama3:latest",
            Stream = false,
            Messages =
            [
                new OllamaMessage
                {
                    Role = "system",
                    //Content = DottorePrompt.SystemPrompt
                },

                new OllamaMessage
                {
                    Role = "user",
                    Content = request.Message
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/chat",
            ollamaRequest);

        response.EnsureSuccessStatusCode();

        var ollamaResponse =
            await response.Content.ReadFromJsonAsync<OllamaChatResponse>();

        return new ChatResponse
        {
            Response = ollamaResponse?.Message.Content
                ?? "No response received."
        };
    }
}