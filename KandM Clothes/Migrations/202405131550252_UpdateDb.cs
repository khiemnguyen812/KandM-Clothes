namespace KandM_Clothes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo._tb_New", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo._tb_New", "Image");
        }
    }
}
