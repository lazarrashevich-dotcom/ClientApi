using ClientApi.Models;
using ClientApi.Models.Enums;
using ClientApi.Rules;

namespace ClientApi.Services;

public interface IClientService
{
    IReadOnlyList<ClientProfile> GetAll();
    ClientProfile? GetById(int id);
    ClientProfile Add(ClientRequest req);
    ClientRecommendation Analyze(ClientRequest req);
}

public class ClientService : IClientService
{
    private readonly List<ClientProfile> _store = new();
    private readonly List<IClientRule> _rules;
    private int _nextId = 1;

    public ClientService()
    {
        _rules = new List<IClientRule>
        {
            new BalanceRule(),
            new ActivityRule(),
            new RiskRule()
        };
    }

    public IReadOnlyList<ClientProfile> GetAll() => _store.AsReadOnly();

    public ClientProfile? GetById(int id) => _store.FirstOrDefault(c => c.Id == id);

    public ClientProfile Add(ClientRequest req)
    {
        var recommendation = Analyze(req);
        var profile = new ClientProfile
        {
            Id                 = _nextId++,
            FirstName          = req.FirstName,
            LastName           = req.LastName,
            Email              = req.Email,
            Balance            = req.Balance,
            AccountAgeDays     = req.AccountAgeDays,
            Status             = req.Status,
            HasOverduePayments = req.HasOverduePayments,
            Category           = recommendation.Category,
            Risk               = recommendation.Risk,
            CreatedAt          = DateTime.UtcNow
        };
        _store.Add(profile);
        return profile;
    }

    public ClientRecommendation Analyze(ClientRequest req)
    {
        int score = _rules.Sum(r => r.Calculate(req));
        score = Math.Clamp(score, 0, 100);
        return new ClientRecommendation
        {
            Score       = score,
            Category    = ScoreRule.Classify(score),
            Risk        = ScoreRule.AssessRisk(req),
            Advice      = ScoreRule.GetAdvice(score, req)
        };
    }
}
