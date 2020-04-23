using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreatsParser.Core.DataBase.Models
{
    public class ThreatSources
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Threat")]
        public Guid ThreatId { get; set; }

        public Threats Threat { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Source { get; set; }
    }
}