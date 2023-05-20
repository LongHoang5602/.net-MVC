namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Category", newName: "Categories");
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
            RenameTable(name: "dbo.Categories", newName: "Category");
        }
    }
}
