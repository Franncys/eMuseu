namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testeV45 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emp_Peca", "data_Entregue", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emp_Peca", "data_Entregue");
        }
    }
}
