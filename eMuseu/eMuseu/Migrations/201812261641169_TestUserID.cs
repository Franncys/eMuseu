namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprestimos", "userID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emprestimos", "userID");
        }
    }
}
