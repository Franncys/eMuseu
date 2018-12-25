namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mensagem_modeloV3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mensagem", "EmailDest", c => c.String(nullable: false));
            AlterColumn("dbo.Mensagem", "Msg", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mensagem", "Msg", c => c.String());
            AlterColumn("dbo.Mensagem", "EmailDest", c => c.String());
        }
    }
}
