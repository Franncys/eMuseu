namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleV3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DataNascimento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime());
        }
    }
}
