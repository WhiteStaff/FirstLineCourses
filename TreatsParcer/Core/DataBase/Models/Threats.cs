using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreatsParser.Core.DataBase.Models
{
    public class Threats
    {
        [Key]
        public Guid Id { get; set; }

        public int ThreatId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsHasPrivacyViolation { get; set; }

        public bool IsHasIntegrityViolation { get; set; }

        public bool IsHasAvailabilityViolation { get; set; }

        public ICollection<ThreatSources> Sources { get; set; }

        public ICollection<ThreatTargets> Targets { get; set; }

    }
}