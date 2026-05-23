namespace Data;

public class DeliveryLog
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string BriefSummary { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
