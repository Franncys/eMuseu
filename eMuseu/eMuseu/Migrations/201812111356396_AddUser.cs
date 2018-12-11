namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprestimos", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Emprestimos", "UserId");
            AddForeignKey("dbo.Emprestimos", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emprestimos", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Emprestimos", new[] { "UserId" });
            DropColumn("dbo.Emprestimos", "UserId");
        }
    }
}
