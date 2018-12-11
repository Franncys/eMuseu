namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NomeU", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Morada", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Cidade", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserTipo", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "aprovado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "aprovado");
            DropColumn("dbo.AspNetUsers", "UserTipo");
            DropColumn("dbo.AspNetUsers", "Cidade");
            DropColumn("dbo.AspNetUsers", "Morada");
            DropColumn("dbo.AspNetUsers", "DataNascimento");
            DropColumn("dbo.AspNetUsers", "NomeU");
        }
    }
}
