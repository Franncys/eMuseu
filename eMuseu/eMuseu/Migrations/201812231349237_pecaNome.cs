namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pecaNome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pecas", "nomePeca", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pecas", "nomePeca");
        }
    }
}
