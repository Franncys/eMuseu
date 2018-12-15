namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roleV3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserTipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserTipo", c => c.Int(nullable: false));
        }
    }
}
