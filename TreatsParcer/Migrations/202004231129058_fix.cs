namespace ThreatsParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ThreatTargets");
            AlterColumn("dbo.ThreatTargets", "Type", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ThreatTargets", new[] {"ThreatId", "Type"});
        }


        public override void Down()
        {
            DropPrimaryKey("dbo.ThreatTargets");
            AlterColumn("dbo.ThreatTargets", "Type", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ThreatTargets", new[] { "ThreatId", "Type" });
        }
    }
}
