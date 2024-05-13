namespace KandM_Clothes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo._tb_Category", "Title", c => c.String(nullable: false));
            AlterColumn("dbo._tb_Category", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo._tb_Category", "Description", c => c.String());
            AlterColumn("dbo._tb_Category", "Title", c => c.String());
        }
    }
}
