namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigrationv6_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime(nullable: false));
        }
    }
}
