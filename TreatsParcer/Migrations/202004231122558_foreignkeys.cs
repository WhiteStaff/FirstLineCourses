namespace ThreatsParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeys : DbMigration
    {
        public override void Up()
        {
          
           CreateTable(
                   "dbo.ThreatSources",
                   c => new
                   {
                       ThreatId = c.Int(nullable: false),
                       Source = c.Int(nullable: false)
                   })
               .PrimaryKey(t => new { t.ThreatId, t.Source })
               .ForeignKey("dbo.Threats", t => t.ThreatId)
               .Index(t => t.ThreatId);

           CreateTable(
                   "dbo.ThreatTargets",
                   c => new
                   {
                       ThreatId = c.Int(nullable: false),
                       Type = c.Int(nullable: false)
                   })
               .PrimaryKey(t => new { t.ThreatId, t.Type })
               .ForeignKey("dbo.Threats", t => t.ThreatId)
               .Index(t => t.ThreatId);

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
            DropForeignKey("dbo.ThreatTargets", "ThreatId", "dbo.Threats");
            DropForeignKey("dbo.ThreatSources", "ThreatId", "dbo.Threats");
            DropIndex("dbo.ThreatTargets", new[] { "ThreatId" });
            DropIndex("dbo.ThreatSources", new[] { "ThreatId" });
            DropPrimaryKey("dbo.ThreatTargets");
            DropPrimaryKey("dbo.ThreatSources");
            AlterColumn("dbo.ThreatTargets", "ThreatId", c => c.Int());
            AlterColumn("dbo.ThreatSources", "ThreatId", c => c.Int());
            AddPrimaryKey("dbo.ThreatTargets", new[] { "ThreatId", "Type" });
            AddPrimaryKey("dbo.ThreatSources", new[] { "ThreatId", "Source" });
            RenameColumn(table: "dbo.ThreatTargets", name: "ThreatId", newName: "Threats_Id");
            RenameColumn(table: "dbo.ThreatSources", name: "ThreatId", newName: "Threats_Id");
            AddColumn("dbo.ThreatTargets", "ThreatId", c => c.Int(nullable: false));
            AddColumn("dbo.ThreatSources", "ThreatId", c => c.Int(nullable: false));
            CreateIndex("dbo.ThreatTargets", "Threats_Id");
            CreateIndex("dbo.ThreatSources", "Threats_Id");
            AddForeignKey("dbo.ThreatTargets", "Threats_Id", "dbo.Threats", "Id");
            AddForeignKey("dbo.ThreatSources", "Threats_Id", "dbo.Threats", "Id");
        }
    }
}
