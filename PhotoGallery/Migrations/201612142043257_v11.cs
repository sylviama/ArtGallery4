namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arts", "Fake", c => c.String(nullable: false));
            AlterColumn("dbo.Arts", "ArtName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arts", "ArtName", c => c.String(nullable: false));
            DropColumn("dbo.Arts", "Fake");
        }
    }
}
