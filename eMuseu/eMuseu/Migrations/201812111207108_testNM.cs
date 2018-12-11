namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testNM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emprestimos",
                c => new
                    {
                        EmprestimoID = c.Int(nullable: false, identity: true),
                        data_inicio = c.DateTime(nullable: false),
                        data_fim = c.DateTime(nullable: false),
                        validado = c.Boolean(nullable: false),
                        devolvido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmprestimoID);
            
            CreateTable(
                "dbo.EmprestimoPecas",
                c => new
                    {
                        Emprestimo_EmprestimoID = c.Int(nullable: false),
                        Peca_PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emprestimo_EmprestimoID, t.Peca_PecaID })
                .ForeignKey("dbo.Emprestimos", t => t.Emprestimo_EmprestimoID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.Peca_PecaID, cascadeDelete: true)
                .Index(t => t.Emprestimo_EmprestimoID)
                .Index(t => t.Peca_PecaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos");
            DropIndex("dbo.EmprestimoPecas", new[] { "Peca_PecaID" });
            DropIndex("dbo.EmprestimoPecas", new[] { "Emprestimo_EmprestimoID" });
            DropTable("dbo.EmprestimoPecas");
            DropTable("dbo.Emprestimos");
        }
    }
}
