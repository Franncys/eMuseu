namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TesteFCK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensagem", "EmailOri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensagem", "EmailOri");
        }
    }
}
