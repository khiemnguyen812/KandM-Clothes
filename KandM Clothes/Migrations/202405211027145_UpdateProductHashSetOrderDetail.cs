namespace KandM_Clothes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductHashSetOrderDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo._tb_Product", "ProductCategory_Id", "dbo._tb_ProductCategory");
            DropIndex("dbo._tb_Product", new[] { "ProductCategory_Id" });
            AlterColumn("dbo._tb_Product", "ProductCategory_Id", c => c.Int(nullable: false));
            CreateIndex("dbo._tb_Product", "ProductCategory_Id");
            AddForeignKey("dbo._tb_Product", "ProductCategory_Id", "dbo._tb_ProductCategory", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo._tb_Product", "ProductCategory_Id", "dbo._tb_ProductCategory");
            DropIndex("dbo._tb_Product", new[] { "ProductCategory_Id" });
            AlterColumn("dbo._tb_Product", "ProductCategory_Id", c => c.Int());
            CreateIndex("dbo._tb_Product", "ProductCategory_Id");
            AddForeignKey("dbo._tb_Product", "ProductCategory_Id", "dbo._tb_ProductCategory", "Id");
        }
    }
}
