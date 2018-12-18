namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleV4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DataNascimento");
        }
    }
}
