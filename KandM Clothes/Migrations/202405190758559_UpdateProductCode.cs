namespace KandM_Clothes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo._tb_Product", "ProductCode", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo._tb_Product", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo._tb_Product", "Image", c => c.String());
            DropColumn("dbo._tb_Product", "ProductCode");
        }
    }
}
