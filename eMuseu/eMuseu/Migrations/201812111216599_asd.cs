namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilizadores", "Nome", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizadores", "Nome", c => c.Int(nullable: false));
        }
    }
}
