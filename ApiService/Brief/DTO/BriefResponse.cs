namespace ApiService.Brief.DTO;

public record BriefResponse(
    string BriefHtml,
    string WhatsAppText,
    List<string> Tldr);
