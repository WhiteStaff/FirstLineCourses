using System.Data.Entity;
using TreatsParser.Core.DataBase.Models;

namespace TreatsParser.Core.DataBase
{
    public class ThreatDbContext : DbContext
    {
        public ThreatDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Threats> Threat { get; set; }

        public DbSet<ThreatSources> Source { get; set; }

        public DbSet<ThreatTargets> Target { get; set; }

    }
}