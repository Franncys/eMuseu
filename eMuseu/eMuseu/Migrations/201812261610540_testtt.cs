namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pecas", "Estado", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pecas", "Estado");
        }
    }
}
