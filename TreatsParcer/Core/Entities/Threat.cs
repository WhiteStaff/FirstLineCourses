using System.Text;
using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Entities
{
    class Threat
    {
        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Source { get; }

        public string ExposureSubject { get; }

        public bool IsHasPrivacyViolation { get; }

        public bool IsHasIntegrityViolation { get; }

        public bool IsHasAvailabilityViolation { get; }

        public RiskProbabilities RiskProbabilities { get; set; }

        public Threat(string[] rowValues)
        {
            Id = int.Parse(rowValues[0]);
            Name = rowValues[1].Replace("_x000d_", "\n");
            Description = rowValues[2].Replace("_x000d_", "\n");
            Source = rowValues[3].Replace("_x000d_", "\n") ;
            ExposureSubject = rowValues[4].Replace("_x000d_", "\n");
            IsHasPrivacyViolation = rowValues[5].Equals("1");
            IsHasIntegrityViolation = rowValues[6].Equals("1");
            IsHasAvailabilityViolation = rowValues[7].Equals("1");
        }

        public string[] GetValuesAsArray()
        {
            var privacy = (IsHasPrivacyViolation
                ? "Присутствует"
                : "Отсутствует");
            var integrity = (IsHasPrivacyViolation
                ? "Присутствует"
                : "Отсутствует");
            var available = (IsHasPrivacyViolation
                ? "Присутствует"
                : "Отсутствует");
            return new[] {$"УБИ.{Id}", $"{Name}", $"{Description}", $"{Source}", $"{ExposureSubject}", privacy, integrity, available};
        }

        public override string ToString()
        {
            var x = new StringBuilder();
            x.AppendLine($"Идентификатор угрозы: УБИ.{Id}");
            x.AppendLine();
            x.AppendLine($"Название угрозы: {Name}");
            x.AppendLine();
            x.AppendLine($"Описание: {Description}");
            x.AppendLine();
            x.AppendLine($"Источник: {Source}");
            x.AppendLine();
            x.AppendLine($"Объект воздействия: {ExposureSubject}");
            x.AppendLine();
            x.AppendLine(IsHasPrivacyViolation
                ? "Нарушение конфиденциальности присутствует"
                : "Нарушение конфиденциальности отсутствует");
            x.AppendLine();
            x.AppendLine(IsHasIntegrityViolation
                ? "Нарушение целостности присутствует"
                : "Нарушение целостности отсутствует");
            x.AppendLine();
            x.AppendLine(IsHasAvailabilityViolation
                ? "Нарушение доступности присутствует"
                : "Нарушение доступности отсутствует");
            return x.ToString();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Threat threat)) return false;
            var x1 = Id == threat.Id;
            var x2 = Name.Replace("\r", "").Replace("\n", "") == threat.Name.Replace("\r", "").Replace("\n", "");
            var x3 = Description.Replace("\r", "").Replace("\n", "") == threat.Description.Replace("\r", "").Replace("\n", "");
            var x4 = Source.Replace("\r", "").Replace("\n", "") == threat.Source.Replace("\r", "").Replace("\n", "");
            var x5 = ExposureSubject.Replace("\r", "").Replace("\n", "") == threat.ExposureSubject.Replace("\r", "").Replace("\n", "");
            var x6 = IsHasPrivacyViolation == threat.IsHasPrivacyViolation;
            var x7 = IsHasAvailabilityViolation == threat.IsHasAvailabilityViolation;
            var x8 = IsHasIntegrityViolation == threat.IsHasIntegrityViolation;
            return x1 && x2 && x3 && x4 && x5 && x6 && x7 && x8;
        }
    }
}