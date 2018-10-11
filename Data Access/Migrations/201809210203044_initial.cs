namespace Data_Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 200),
                        Price = c.Single(nullable: false),
                        Total_In_Shelf = c.Single(nullable: false),
                        Total_In_Vault = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "StoreId", "dbo.Stores");
            DropIndex("dbo.Articles", new[] { "StoreId" });
            DropTable("dbo.Stores");
            DropTable("dbo.Articles");
        }
    }
}
