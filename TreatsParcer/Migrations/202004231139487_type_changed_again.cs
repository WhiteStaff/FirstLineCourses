namespace ThreatsParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class type_changed_again : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ThreatSources");
            AlterColumn("dbo.ThreatSources", "Source", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ThreatSources", new[] { "ThreatId", "Source" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ThreatSources");
            AlterColumn("dbo.ThreatSources", "Source", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ThreatSources", new[] { "ThreatId", "Source" });
        }
    }
}
