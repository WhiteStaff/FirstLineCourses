using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using ThreatsParser.Entities;
using TreatsParser.Core.Helpers;

namespace TreatsParser.Core
{
    public static class ModelGeneration
    {
        public static List<ModelLine> GenerateModelForPreview(List<Threat> items, double GlobalCoef)
        {
            var _model = new List<ModelLine>();
            _model = items.Select((threat, i) => new ModelLine()
            {
                Id = i+1,
                ThreatNumber = $"УБИ.{threat.Id}",
                Y2 = (int)threat.RiskProbabilities ,
                Y = (GlobalCoef + (int)threat.RiskProbabilities)/20,
                DangerLevel = Resolver.ResolveDanger((double)((int)threat.RiskProbabilities)/ GlobalCoef),
                isActual = Resolver.ResolveActual(Resolver.ResolveDanger((double)((int)threat.RiskProbabilities) / GlobalCoef), threat.RiskProbabilities).ToString()
            }).ToList();

            return _model;
        }
    }
}