namespace ApiService.Brief;

using System.Text;
using System.Text.Json;
using Brief.DTO;

public class BriefClient(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true
    };

    public async Task<BriefResponse> GenerateBriefAsync(Guid userId, int historyBiteIndex)
    {
        var payload = new { user_id = userId, history_bite_index = historyBiteIndex };
        return await PostAsync("/brief/generate", payload);
    }

    public async Task<BriefResponse> GenerateOnboardingAsync(Guid userId)
    {
        var payload = new { user_id = userId };
        return await PostAsync("/brief/onboarding", payload);
    }

    private async Task<BriefResponse> PostAsync(string path, object payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(path, content);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<BriefResponse>(body, s_jsonOptions)
            ?? throw new InvalidOperationException("Empty response from PythonApi");
    }
}
