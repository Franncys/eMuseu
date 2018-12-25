namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TesteTesteasd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emprestimos", "data_inicio", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emprestimos", "data_inicio", c => c.DateTime(nullable: false));
        }
    }
}
