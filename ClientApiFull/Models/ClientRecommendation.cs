using ClientApi.Models.Enums;

namespace ClientApi.Models;

public class ClientRecommendation
{
    public int Score { get; set; }
    public ClientCategory Category { get; set; }
    public RiskLevel Risk { get; set; }
    public string Advice { get; set; } = string.Empty;
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
