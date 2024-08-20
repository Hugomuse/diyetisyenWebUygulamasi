namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ilans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiyetisyenID = c.Int(nullable: false),
                        Baslik = c.String(),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IlanID = c.Int(nullable: false),
                        MedyaYolu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
            DropTable("dbo.Ilans");
        }
    }
}
