namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalDBV2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tratamentos", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tratamentos", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
