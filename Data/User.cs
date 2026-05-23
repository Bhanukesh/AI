namespace Data;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string WhatsAppNumber { get; set; } = string.Empty;
    public bool IsNewUser { get; set; } = true;
    public int HistoryBiteIndex { get; set; } = 0;
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    public DateTime? LastSentAt { get; set; }
}
