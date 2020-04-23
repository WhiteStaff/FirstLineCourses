namespace ThreatsParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThreatSources",
                c => new
                    {
                        ThreatId = c.Int(nullable: false),
                        Source = c.Int(nullable: false),
                        Threats_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.ThreatId, t.Source })
                .ForeignKey("dbo.Threats", t => t.Threats_Id)
                .Index(t => t.Threats_Id);
            
            CreateTable(
                "dbo.ThreatTargets",
                c => new
                    {
                        ThreatId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Threats_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.ThreatId, t.Type })
                .ForeignKey("dbo.Threats", t => t.Threats_Id)
                .Index(t => t.Threats_Id);
            
            CreateTable(
                "dbo.Threats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
            DropForeignKey("dbo.ThreatTargets", "Threats_Id", "dbo.Threats");
            DropForeignKey("dbo.ThreatSources", "Threats_Id", "dbo.Threats");
            DropIndex("dbo.ThreatTargets", new[] { "Threats_Id" });
            DropIndex("dbo.ThreatSources", new[] { "Threats_Id" });
            DropTable("dbo.Threats");
            DropTable("dbo.ThreatTargets");
            DropTable("dbo.ThreatSources");
        }
    }
}
