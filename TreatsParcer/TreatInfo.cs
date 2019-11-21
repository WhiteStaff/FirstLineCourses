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

            public TreatInfo(string id, string name, string description, string source, string exposureSubject,
                string isHasPrivacyViolation, string isHasIntegrityViolation, string isHasAvailabilityViolation)
            {

            }

            public TreatInfo(string[] rowValues)
            {
                Id = rowValues[0];
                Name = rowValues[1];
                Description = rowValues[2];
                Source = rowValues[3];
                ExposureSubject = rowValues[4];
                IsHasPrivacyViolation = rowValues[5].Equals("1");
                IsHasIntegrityViolation = rowValues[6].Equals("1");
                IsHasAvailabilityViolation = rowValues[7].Equals("1");
            }
        }

   
}
