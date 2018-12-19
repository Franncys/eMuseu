namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PecaEmprestimo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos");
            DropForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas");
            DropIndex("dbo.EmprestimoPecas", new[] { "Emprestimo_EmprestimoID" });
            DropIndex("dbo.EmprestimoPecas", new[] { "Peca_PecaID" });
            CreateTable(
                "dbo.Emp_Peca",
                c => new
                    {
                        PecaID = c.Int(nullable: false),
                        EmprestimoID = c.Int(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => new { t.PecaID, t.EmprestimoID })
                .ForeignKey("dbo.Emprestimos", t => t.EmprestimoID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.PecaID, cascadeDelete: true)
                .Index(t => t.PecaID)
                .Index(t => t.EmprestimoID);
            
            DropTable("dbo.EmprestimoPecas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmprestimoPecas",
                c => new
                    {
                        Emprestimo_EmprestimoID = c.Int(nullable: false),
                        Peca_PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emprestimo_EmprestimoID, t.Peca_PecaID });
            
            DropForeignKey("dbo.Emp_Peca", "PecaID", "dbo.Pecas");
            DropForeignKey("dbo.Emp_Peca", "EmprestimoID", "dbo.Emprestimos");
            DropIndex("dbo.Emp_Peca", new[] { "EmprestimoID" });
            DropIndex("dbo.Emp_Peca", new[] { "PecaID" });
            DropTable("dbo.Emp_Peca");
            CreateIndex("dbo.EmprestimoPecas", "Peca_PecaID");
            CreateIndex("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID");
            AddForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas", "PecaID", cascadeDelete: true);
            AddForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos", "EmprestimoID", cascadeDelete: true);
        }
    }
}
