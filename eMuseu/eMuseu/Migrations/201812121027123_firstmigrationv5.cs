namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "aprovado", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "RoleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RoleName");
            DropColumn("dbo.AspNetUsers", "aprovado");
        }
    }
}
