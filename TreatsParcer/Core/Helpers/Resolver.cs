using ThreatsParser.Entities.Enums;

namespace TreatsParser.Core.Helpers
{
    public static class Resolver
    {
        public static DangerLevel ResolveDanger(double coef)
        {
            if (coef <= 0.3) return DangerLevel.Low;
            if (coef <= 0.6) return DangerLevel.Medium;
            if (coef <= 0.8) return DangerLevel.High;
            return DangerLevel.VeryHigh;
        }

        public static bool ResolveActual(DangerLevel dangerLevel, RiskProbabilities risk)
        {
            if (risk == RiskProbabilities.Unlikely) return false;
            if ((dangerLevel == DangerLevel.Low || dangerLevel == DangerLevel.Medium) &&
                risk == RiskProbabilities.Low) return false;
            if (dangerLevel == DangerLevel.Low && risk == RiskProbabilities.Medium) return false;

            return true;
        }
    }
}