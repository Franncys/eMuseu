namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mensagem_modeloV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensagem", "EmailDest", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensagem", "EmailDest");
        }
    }
}
