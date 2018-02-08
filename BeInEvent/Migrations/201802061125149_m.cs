namespace BeInEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Image", c => c.String(nullable: false));
        }
    }
}
