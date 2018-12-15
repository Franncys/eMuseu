namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "NomeP", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "NomeP", c => c.String(nullable: false));
        }
    }
}
