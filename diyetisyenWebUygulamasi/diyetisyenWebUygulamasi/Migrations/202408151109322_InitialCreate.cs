namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diyetisyens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        Aciklama = c.String(),
                        ResimYolu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kullanicis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(),
                        ResimYolu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Randevus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiyetisyenID = c.Int(nullable: false),
                        KullaniciID = c.Int(nullable: false),
                        tarih = c.DateTime(nullable: false),
                        Not = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Siparis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciID = c.Int(nullable: false),
                        DiyetisyenID = c.Int(nullable: false),
                        YorumYapildi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Yorums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Yildiz = c.Int(nullable: false),
                        Icerik = c.String(),
                        Baslik = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Yorums");
            DropTable("dbo.Siparis");
            DropTable("dbo.Randevus");
            DropTable("dbo.Kullanicis");
            DropTable("dbo.Diyetisyens");
        }
    }
}
