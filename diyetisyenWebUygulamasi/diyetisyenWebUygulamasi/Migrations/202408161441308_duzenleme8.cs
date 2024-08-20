namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Begenis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiyetisyenID = c.Int(nullable: false),
                        KullaniciID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Yorums", "DiyetisyenId", c => c.Int(nullable: false));
            AddColumn("dbo.Yorums", "KullaniciId", c => c.Int(nullable: false));
            DropColumn("dbo.Yorums", "Baslik");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Yorums", "Baslik", c => c.String());
            DropColumn("dbo.Yorums", "KullaniciId");
            DropColumn("dbo.Yorums", "DiyetisyenId");
            DropTable("dbo.Begenis");
        }
    }
}
