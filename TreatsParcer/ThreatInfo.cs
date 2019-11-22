using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatsParcer
{
    class ThreatInfo
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Source { get; }
        public string ExposureSubject { get; }
        public bool IsHasPrivacyViolation { get; }
        public bool IsHasIntegrityViolation { get; }
        public bool IsHasAvailabilityViolation { get; }

        public ThreatInfo(string[] rowValues)
        {
            Id = int.Parse(rowValues[0]);
            Name = rowValues[1].Replace("_x000d_", "\n"); ;
            Description = rowValues[2].Replace("_x000d_", "\n");
            Source = rowValues[3].Replace("_x000d_", "\n"); ;
            ExposureSubject = rowValues[4].Replace("_x000d_", "\n"); ;
            IsHasPrivacyViolation = rowValues[5].Equals("1");
            IsHasIntegrityViolation = rowValues[6].Equals("1");
            IsHasAvailabilityViolation = rowValues[7].Equals("1");
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
    }
}