using System.Text;

namespace ThreatsParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.ThreatSources",
                    c => new
                    {
                        ThreatId = c.Guid(nullable: false),
                        Source = c.String(nullable: false, maxLength: 128)
                    })
                .PrimaryKey(t => new { t.ThreatId, t.Source })
                .ForeignKey("dbo.Threats", t => t.ThreatId)
                .Index(t => t.ThreatId);

            CreateTable(
                    "dbo.ThreatTargets",
                    c => new
                    {
                        ThreatId = c.Guid(nullable: false),
                        Type = c.String(nullable: false, maxLength: 128)
                    })
                .PrimaryKey(t => new { t.ThreatId, t.Type })
                .ForeignKey("dbo.Threats", t => t.ThreatId)
                .Index(t => t.ThreatId);

            CreateTable(
                    "dbo.Threats",
                    c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ThreatId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        IsHasPrivacyViolation = c.Boolean(nullable: false),
                        IsHasIntegrityViolation = c.Boolean(nullable: false),
                        IsHasAvailabilityViolation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
        }
    }
}
