namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emprestimos_Pecas", "PecaID", "dbo.Emprestimos");
            DropForeignKey("dbo.Emprestimos", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Emprestimos_Pecas", "PecaID", "dbo.Pecas");
            DropForeignKey("dbo.Rececoes", "PecaID_PecaID", "dbo.Pecas");
            DropIndex("dbo.Emprestimos_Pecas", new[] { "PecaID" });
            DropIndex("dbo.Emprestimos", new[] { "UserId" });
            DropIndex("dbo.Rececoes", new[] { "PecaID_PecaID" });
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
            
            AddColumn("dbo.AspNetUsers", "NomeP", c => c.String());
            AddColumn("dbo.AspNetUsers", "NomeU", c => c.String());
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Cidade", c => c.String());
            AddColumn("dbo.AspNetUsers", "Morada", c => c.String());
            AddColumn("dbo.AspNetUsers", "aprovado", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "RoleName", c => c.String());
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Emprestimos", "UserId");
            DropTable("dbo.Emprestimos_Pecas");
            DropTable("dbo.Rececoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rececoes",
                c => new
                    {
                        rececaoID = c.Int(nullable: false, identity: true),
                        PecaID_PecaID = c.Int(),
                    })
                .PrimaryKey(t => t.rececaoID);
            
            CreateTable(
                "dbo.Emprestimos_Pecas",
                c => new
                    {
                        EmprestimoID = c.Int(nullable: false),
                        PecaID = c.Int(nullable: false),
                        deterioracao = c.String(),
                    })
                .PrimaryKey(t => new { t.EmprestimoID, t.PecaID });
            
            AddColumn("dbo.Emprestimos", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos");
            DropIndex("dbo.EmprestimoPecas", new[] { "Peca_PecaID" });
            DropIndex("dbo.EmprestimoPecas", new[] { "Emprestimo_EmprestimoID" });
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetUsers", "RoleName");
            DropColumn("dbo.AspNetUsers", "aprovado");
            DropColumn("dbo.AspNetUsers", "Morada");
            DropColumn("dbo.AspNetUsers", "Cidade");
            DropColumn("dbo.AspNetUsers", "DataNascimento");
            DropColumn("dbo.AspNetUsers", "NomeU");
            DropColumn("dbo.AspNetUsers", "NomeP");
            DropTable("dbo.EmprestimoPecas");
            CreateIndex("dbo.Rececoes", "PecaID_PecaID");
            CreateIndex("dbo.Emprestimos", "UserId");
            CreateIndex("dbo.Emprestimos_Pecas", "PecaID");
            AddForeignKey("dbo.Rececoes", "PecaID_PecaID", "dbo.Pecas", "PecaID");
            AddForeignKey("dbo.Emprestimos_Pecas", "PecaID", "dbo.Pecas", "PecaID", cascadeDelete: true);
            AddForeignKey("dbo.Emprestimos", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Emprestimos_Pecas", "PecaID", "dbo.Emprestimos", "EmprestimoID", cascadeDelete: true);
        }
    }
}
