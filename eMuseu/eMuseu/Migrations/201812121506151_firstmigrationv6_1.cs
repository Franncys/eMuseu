namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv6_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "NomeU", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Cidade", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Morada", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Morada", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cidade", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "NomeU", c => c.String(nullable: false));
        }
    }
}
