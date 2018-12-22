namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationalTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rececoes",
                c => new
                    {
                        rececaoID = c.Int(nullable: false, identity: true),
                        formulario = c.String(),
                        antes = c.String(),
                        depois = c.String(),
                        cumprimento = c.Int(nullable: false),
                        PecaID_PecaID = c.Int(),
                    })
                .PrimaryKey(t => t.rececaoID)
                .ForeignKey("dbo.Pecas", t => t.PecaID_PecaID)
                .Index(t => t.PecaID_PecaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rececoes", "PecaID_PecaID", "dbo.Pecas");
            DropIndex("dbo.Rececoes", new[] { "PecaID_PecaID" });
            DropTable("dbo.Rececoes");
        }
    }
}
