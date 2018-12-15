namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NOmeU", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Cidade", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Morada", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserTipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserTipo");
            DropColumn("dbo.AspNetUsers", "Morada");
            DropColumn("dbo.AspNetUsers", "Cidade");
            DropColumn("dbo.AspNetUsers", "DataNascimento");
            DropColumn("dbo.AspNetUsers", "NOmeU");
        }
    }
}
