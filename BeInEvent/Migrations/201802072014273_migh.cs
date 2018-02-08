namespace BeInEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" }, "dbo.UserCategories");
            DropIndex("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" });
            AlterColumn("dbo.AspNetUsers", "UserCategory_UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UserCategory_CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" });
            AddForeignKey("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" }, "dbo.UserCategories", new[] { "UserID", "CategoryID" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" }, "dbo.UserCategories");
            DropIndex("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" });
            AlterColumn("dbo.AspNetUsers", "UserCategory_CategoryID", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "UserCategory_UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" });
            AddForeignKey("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" }, "dbo.UserCategories", new[] { "UserID", "CategoryID" });
        }
    }
}
