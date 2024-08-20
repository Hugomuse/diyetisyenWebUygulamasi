namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "DiyetisyenId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ResimMi", c => c.Boolean(nullable: false));
            DropColumn("dbo.Posts", "IlanID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "IlanID", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "ResimMi");
            DropColumn("dbo.Posts", "DiyetisyenId");
        }
    }
}
