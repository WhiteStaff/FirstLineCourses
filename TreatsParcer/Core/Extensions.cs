using System;
using System.Data.Entity;
using System.Linq;
using ThreatsParser.Entities;
using TreatsParser.Core.DataBase.Models;

namespace TreatsParser.Core
{
    public static class Extensions
    {
        public static Threats ToDbModel(this Threat threat)
        {
            var guid = Guid.NewGuid();
            return new Threats
            {
                Id = guid,
                ThreatId = threat.Id,
                Description = threat.Description,
                Name = threat.Name,
                IsHasAvailabilityViolation = threat.IsHasAvailabilityViolation,
                IsHasIntegrityViolation = threat.IsHasIntegrityViolation,
                IsHasPrivacyViolation = threat.IsHasPrivacyViolation,
                Sources = threat.Source.Select(x => new ThreatSources{ThreatId = guid, Source = x}).ToList(),
                Targets = threat.ExposureSubject.Select(x => new ThreatTargets{ThreatId = guid, Type = x}).ToList(),
            };
        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }

        public static Threat ToEntity(this Threats value)
        {
            return new Threat(value);
        }
    }
}