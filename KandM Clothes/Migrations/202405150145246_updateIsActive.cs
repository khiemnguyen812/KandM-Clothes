namespace KandM_Clothes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo._tb_Advertisement", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_Category", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_New", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_Post", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_Contact", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_Order", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_Product", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo._tb_ProductCategory", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo._tb_ProductCategory", "isActive");
            DropColumn("dbo._tb_Product", "isActive");
            DropColumn("dbo._tb_Order", "isActive");
            DropColumn("dbo._tb_Contact", "isActive");
            DropColumn("dbo._tb_Post", "isActive");
            DropColumn("dbo._tb_New", "isActive");
            DropColumn("dbo._tb_Category", "isActive");
            DropColumn("dbo._tb_Advertisement", "isActive");
        }
    }
}
