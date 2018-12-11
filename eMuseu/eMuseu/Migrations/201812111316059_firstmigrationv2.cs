namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NomeP", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NomeP");
        }
    }
}
