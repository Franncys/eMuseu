namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestUserID1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emprestimos", "userID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emprestimos", "userID", c => c.Int(nullable: false));
        }
    }
}
