using ClientApi.Models.Enums;

namespace ClientApi.Models;

public class ClientProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public int AccountAgeDays { get; set; }
    public ClientStatus Status { get; set; }
    public ClientCategory Category { get; set; }
    public RiskLevel Risk { get; set; }
    public bool HasOverduePayments { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
