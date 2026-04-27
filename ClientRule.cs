using ClientApi.Models;

namespace ClientApi.Rules;

public interface IClientRule
{
    int Calculate(ClientRequest req);
}

public class BalanceRule : IClientRule
{
    public int Calculate(ClientRequest req) => req.Balance switch
    {
        >= 100000 => 40,
        >= 30000  => 25,
        >= 5000   => 10,
        _         => 0
    };
}

public class ActivityRule : IClientRule
{
    public int Calculate(ClientRequest req)
    {
        int score = req.AccountAgeDays >= 365 ? 30 : req.AccountAgeDays >= 90 ? 15 : 5;
        if (req.HasOverduePayments) score -= 20;
        return Math.Max(0, score);
    }
}

public class RiskRule : IClientRule
{
    public int Calculate(ClientRequest req)
    {
        int penalty = 0;
        if (req.HasOverduePayments) penalty += 15;
        if (req.Balance < 1000) penalty += 10;
        return -penalty;
    }
}
