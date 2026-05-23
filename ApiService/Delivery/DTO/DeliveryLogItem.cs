namespace ApiService.Delivery.DTO;

public record DeliveryLogItem(
    Guid Id,
    Guid UserId,
    string UserEmail,
    DateTime Date,
    string Channel,
    string Status,
    string BriefSummary,
    DateTime CreatedAt);
