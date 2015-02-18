namespace GamingStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        APIKey = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Release = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InventoryCount = c.Int(nullable: false),
                        Cart_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cart", t => t.Cart_ID)
                .Index(t => t.Cart_ID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GenreGame",
                c => new
                    {
                        Genre_ID = c.Int(nullable: false),
                        Game_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_ID, t.Game_ID })
                .ForeignKey("dbo.Genre", t => t.Genre_ID, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.Game_ID, cascadeDelete: true)
                .Index(t => t.Genre_ID)
                .Index(t => t.Game_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.GenreGame", new[] { "Game_ID" });
            DropIndex("dbo.GenreGame", new[] { "Genre_ID" });
            DropIndex("dbo.Game", new[] { "Cart_ID" });
            DropForeignKey("dbo.GenreGame", "Game_ID", "dbo.Game");
            DropForeignKey("dbo.GenreGame", "Genre_ID", "dbo.Genre");
            DropForeignKey("dbo.Game", "Cart_ID", "dbo.Cart");
            DropTable("dbo.GenreGame");
            DropTable("dbo.Sale");
            DropTable("dbo.Cart");
            DropTable("dbo.Tags");
            DropTable("dbo.Genre");
            DropTable("dbo.Game");
            DropTable("dbo.User");
        }
    }
}
