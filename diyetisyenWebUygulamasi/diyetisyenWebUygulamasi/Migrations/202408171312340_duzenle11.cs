namespace diyetisyenWebUygulamasi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenle11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mesajs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        aliciTipi = c.String(),
                        aliciId = c.Int(nullable: false),
                        vericiId = c.Int(nullable: false),
                        metin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Mesajs");
        }
    }
}
