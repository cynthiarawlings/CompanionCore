using System.Net.Http.Json;
using System.Linq;
using CompanionCore.Core.Interfaces;
using CompanionCore.Core.Models;
using CompanionCore.Infrastructure.Models;

namespace CompanionCore.Infrastructure.Services.AI;

public class OllamaChatService : IChatService
{
    private readonly HttpClient _httpClient;

    public OllamaChatService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Ollama");
    }

    public async Task<ChatResponse> ChatAsync(
        List<ChatMessage> messages)
    {
        var ollamaRequest = new OllamaChatRequest
        {
            Model = "llama3:latest",
            Stream = false,
            Messages = messages
                .Select(m => new OllamaMessage
                {
                    Role = m.Role,
                    Content = m.Content
                })
                .ToList()
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