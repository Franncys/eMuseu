namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pecas",
                c => new
                    {
                        PecaID = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Zona = c.String(),
                        PecaTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PecaID);
            
            CreateTable(
                "dbo.Tratamentos",
                c => new
                    {
                        TratamentoID = c.Int(nullable: false, identity: true),
                        NomeTratamento = c.String(),
                    })
                .PrimaryKey(t => t.TratamentoID);
            
            CreateTable(
                "dbo.Utilizadores",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Nome = c.Int(nullable: false),
                        UserTipo = c.Int(nullable: false),
                        Morada = c.String(),
                        Cidade = c.String(),
                        aprovado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.TratamentosPecas",
                c => new
                    {
                        Tratamentos_TratamentoID = c.Int(nullable: false),
                        Peca_PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tratamentos_TratamentoID, t.Peca_PecaID })
                .ForeignKey("dbo.Tratamentos", t => t.Tratamentos_TratamentoID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.Peca_PecaID, cascadeDelete: true)
                .Index(t => t.Tratamentos_TratamentoID)
                .Index(t => t.Peca_PecaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TratamentosPecas", "Peca_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.TratamentosPecas", "Tratamentos_TratamentoID", "dbo.Tratamentos");
            DropIndex("dbo.TratamentosPecas", new[] { "Peca_PecaID" });
            DropIndex("dbo.TratamentosPecas", new[] { "Tratamentos_TratamentoID" });
            DropTable("dbo.TratamentosPecas");
            DropTable("dbo.Utilizadores");
            DropTable("dbo.Tratamentos");
            DropTable("dbo.Pecas");
        }
    }
}
