using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatsParcer
{
    class TreatInfo
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Source { get; }
        public string ExposureSubject { get; }
        public bool IsHasPrivacyViolation { get; }
        public bool IsHasIntegrityViolation { get; }
        public bool IsHasAvailabilityViolation { get; }

        public TreatInfo(string[] rowValues)
        {
            Id = rowValues[0];
            Name = rowValues[1];
            Description = rowValues[2].Replace("_x000d_", "");
            Source = rowValues[3];
            ExposureSubject = rowValues[4];
            IsHasPrivacyViolation = rowValues[5].Equals("1");
            IsHasIntegrityViolation = rowValues[6].Equals("1");
            IsHasAvailabilityViolation = rowValues[7].Equals("1");
        }

        public override string ToString()
        {
            var x = new StringBuilder();
            x.AppendLine($"ID: {Id}");
            x.AppendLine();
            x.AppendLine($"Name: {Name}");
            x.AppendLine();
            x.AppendLine($"Description: {Description}");
            x.AppendLine();
            x.AppendLine($"Source: {Source}");
            x.AppendLine();
            x.AppendLine($"Exp: {ExposureSubject}");
            x.AppendLine();
            x.AppendLine($"1: {IsHasPrivacyViolation}");
            x.AppendLine();
            x.AppendLine($"2: {IsHasIntegrityViolation}");
            x.AppendLine();
            x.AppendLine($"3: {IsHasAvailabilityViolation}");
            return x.ToString();
        }
    }
}