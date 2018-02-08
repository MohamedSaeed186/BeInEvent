namespace BeInEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 20),
                        User_Category_UserID = c.String(maxLength: 128),
                        User_Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.UserCategories", t => new { t.User_Category_UserID, t.User_Category_CategoryID })
                .Index(t => new { t.User_Category_UserID, t.User_Category_CategoryID });
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 20),
                        Image = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 150),
                        NumberOfTickets = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        City = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        PublisherID = c.String(maxLength: 128),
                        CategoryID = c.Int(nullable: false),
                        EventCanBePublished = c.Int(nullable: false),
                        userEventSubscribEvent_UserID = c.String(maxLength: 128),
                        userEventSubscribEvent_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.UserSubscribeEvents", t => new { t.userEventSubscribEvent_UserID, t.userEventSubscribEvent_EventID })
                .ForeignKey("dbo.AspNetUsers", t => t.PublisherID)
                .Index(t => t.PublisherID)
                .Index(t => t.CategoryID)
                .Index(t => new { t.userEventSubscribEvent_UserID, t.userEventSubscribEvent_EventID });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserFirstName = c.String(),
                        UserLastName = c.String(),
                        UserCity = c.Int(nullable: false),
                        userIsBlocked = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        UserCategory_UserID = c.String(maxLength: 128),
                        UserCategory_CategoryID = c.Int(),
                        userSubscribEvent_UserID = c.String(maxLength: 128),
                        userSubscribEvent_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.UserCity, cascadeDelete: true)
                .ForeignKey("dbo.UserCategories", t => new { t.UserCategory_UserID, t.UserCategory_CategoryID })
                .ForeignKey("dbo.UserSubscribeEvents", t => new { t.userSubscribEvent_UserID, t.userSubscribEvent_EventID })
                .Index(t => t.UserCity)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => new { t.UserCategory_UserID, t.UserCategory_CategoryID })
                .Index(t => new { t.userSubscribEvent_UserID, t.userSubscribEvent_EventID });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        NuOfUserTicket = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TicketID, t.UserID })
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TicketTypeId)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        TicketTypeId = c.Int(nullable: false, identity: true),
                        TypeOfTicket = c.String(nullable: false),
                        TicketPrice = c.Double(),
                        NOTicketType = c.Int(),
                        Event_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketTypeId)
                .ForeignKey("dbo.Events", t => t.Event_EventID)
                .Index(t => t.Event_EventID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserCategories",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.CategoryID });
            
            CreateTable(
                "dbo.UserSubscribeEvents",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        EventID = c.Int(nullable: false),
                        FeedBack = c.String(),
                    })
                .PrimaryKey(t => new { t.UserID, t.EventID });
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        index = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ReportMessage = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.index);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TicketTypes", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.Events", "PublisherID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "userSubscribEvent_UserID", "userSubscribEvent_EventID" }, "dbo.UserSubscribeEvents");
            DropForeignKey("dbo.Events", new[] { "userEventSubscribEvent_UserID", "userEventSubscribEvent_EventID" }, "dbo.UserSubscribeEvents");
            DropForeignKey("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" }, "dbo.UserCategories");
            DropForeignKey("dbo.Categories", new[] { "User_Category_UserID", "User_Category_CategoryID" }, "dbo.UserCategories");
            DropForeignKey("dbo.AspNetUsers", "UserCity", "dbo.Cities");
            DropForeignKey("dbo.Tickets", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.Tickets", "EventID", "dbo.Events");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "CategoryID", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TicketTypes", new[] { "Event_EventID" });
            DropIndex("dbo.Tickets", new[] { "EventID" });
            DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
            DropIndex("dbo.Tickets", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "userSubscribEvent_UserID", "userSubscribEvent_EventID" });
            DropIndex("dbo.AspNetUsers", new[] { "UserCategory_UserID", "UserCategory_CategoryID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "UserCity" });
            DropIndex("dbo.Events", new[] { "userEventSubscribEvent_UserID", "userEventSubscribEvent_EventID" });
            DropIndex("dbo.Events", new[] { "CategoryID" });
            DropIndex("dbo.Events", new[] { "PublisherID" });
            DropIndex("dbo.Categories", new[] { "User_Category_UserID", "User_Category_CategoryID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.UserSubscribeEvents");
            DropTable("dbo.UserCategories");
            DropTable("dbo.Cities");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.Tickets");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
        }
    }
}
