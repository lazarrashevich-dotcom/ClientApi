using ClientApi.Models;
using ClientApi.Models.Enums;

namespace ClientApi.Rules;

public static class ScoreRule
{
    public static ClientCategory Classify(int score) => score switch
    {
        >= 80 => ClientCategory.Platinum,
        >= 60 => ClientCategory.Gold,
        >= 40 => ClientCategory.Silver,
        _     => ClientCategory.Standard
    };

    public static RiskLevel AssessRisk(ClientRequest req)
    {
        if (req.HasOverduePayments && req.Balance < 1000) return RiskLevel.Critical;
        if (req.HasOverduePayments || req.Balance < 5000) return RiskLevel.High;
        if (req.AccountAgeDays < 90) return RiskLevel.Medium;
        return RiskLevel.Low;
    }

    public static string GetAdvice(int score, ClientRequest req) => score switch
    {
        >= 80 => "Предложить Platinum-пакет и персонального менеджера",
        >= 60 => "Предложить Gold-пакет с повышенным кешбэком",
        >= 40 => "Рекомендовать Silver-условия обслуживания",
        _     => "Стандартные условия, мониторинг активности"
    };
}
