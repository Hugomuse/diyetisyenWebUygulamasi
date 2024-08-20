namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Randevus", "Not");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Randevus", "Not", c => c.String());
        }
    }
}
