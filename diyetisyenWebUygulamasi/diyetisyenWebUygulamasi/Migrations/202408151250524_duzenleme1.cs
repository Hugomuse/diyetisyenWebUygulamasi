namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diyetisyens", "Sifre", c => c.String());
            AddColumn("dbo.Kullanicis", "Sifre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanicis", "Sifre");
            DropColumn("dbo.Diyetisyens", "Sifre");
        }
    }
}
