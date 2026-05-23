namespace ApiService.Users.DTO;

public record UserItem(
    Guid Id,
    string Name,
    string Email,
    string WhatsAppNumber,
    bool IsNewUser,
    int HistoryBiteIndex,
    DateTime DateJoined,
    DateTime? LastSentAt);
