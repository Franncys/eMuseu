namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EmprestimoPecas", newName: "PecaEmprestimoes");
            DropPrimaryKey("dbo.PecaEmprestimoes");
            AddPrimaryKey("dbo.PecaEmprestimoes", new[] { "Peca_PecaID", "Emprestimo_EmprestimoID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PecaEmprestimoes");
            AddPrimaryKey("dbo.PecaEmprestimoes", new[] { "Emprestimo_EmprestimoID", "Peca_PecaID" });
            RenameTable(name: "dbo.PecaEmprestimoes", newName: "EmprestimoPecas");
        }
    }
}
