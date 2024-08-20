namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diyetisyens", "ilanBaslik", c => c.String());
            AddColumn("dbo.Diyetisyens", "ilanAciklama", c => c.String());
            DropTable("dbo.Ilans");
        }
        
        public override void Down()
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
            
            DropColumn("dbo.Diyetisyens", "ilanAciklama");
            DropColumn("dbo.Diyetisyens", "ilanBaslik");
        }
    }
}
