namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arts", "Fake", c => c.String());
            AlterColumn("dbo.Arts", "ArtName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arts", "ArtName", c => c.String());
            AlterColumn("dbo.Arts", "Fake", c => c.String(nullable: false));
        }
    }
}
