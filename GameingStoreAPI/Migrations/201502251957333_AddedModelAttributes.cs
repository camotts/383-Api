namespace GamingStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModelAttributes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagsGame",
                c => new
                    {
                        Tags_ID = c.Int(nullable: false),
                        Game_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_ID, t.Game_ID })
                .ForeignKey("dbo.Tags", t => t.Tags_ID, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.Game_ID, cascadeDelete: true)
                .Index(t => t.Tags_ID)
                .Index(t => t.Game_ID);
            
            AddColumn("dbo.Game", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sale", "CartID", c => c.Int(nullable: false));
            DropColumn("dbo.Game", "Release");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Game", "Release", c => c.String());
            DropIndex("dbo.TagsGame", new[] { "Game_ID" });
            DropIndex("dbo.TagsGame", new[] { "Tags_ID" });
            DropForeignKey("dbo.TagsGame", "Game_ID", "dbo.Game");
            DropForeignKey("dbo.TagsGame", "Tags_ID", "dbo.Tags");
            DropColumn("dbo.Sale", "CartID");
            DropColumn("dbo.Game", "ReleaseDate");
            DropTable("dbo.TagsGame");
        }
    }
}
