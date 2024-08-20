namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "ResimMi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ResimMi", c => c.Boolean(nullable: false));
        }
    }
}
