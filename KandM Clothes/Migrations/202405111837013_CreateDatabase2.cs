namespace KandM_Clothes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo._tb_Advertisement", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_Category", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_New", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_Post", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_Contact", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_Order", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_Product", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo._tb_ProductCategory", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo._tb_ProductCategory", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_Product", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_Order", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_Contact", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_Post", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_New", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_Category", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo._tb_Advertisement", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
