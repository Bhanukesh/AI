namespace ApiService.Brief.DTO;

using System.Text.Json.Serialization;

public record BriefResponse(
    string BriefHtml,
    [property: JsonPropertyName("whatsapp_text")] string WhatsAppText,
    List<string> Tldr);
