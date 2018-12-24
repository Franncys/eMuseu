namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mensagem_modelo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensagem",
                c => new
                    {
                        MensagemID = c.Int(nullable: false, identity: true),
                        OrigemID = c.String(),
                        DestinoID = c.String(),
                        Msg = c.String(),
                    })
                .PrimaryKey(t => t.MensagemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Mensagem");
        }
    }
}
