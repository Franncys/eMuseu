namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pecas",
                c => new
                    {
                        PecaID = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Zona = c.String(),
                        PecaTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PecaID);
            
            CreateTable(
                "dbo.Emprestimos",
                c => new
                    {
                        EmprestimoID = c.Int(nullable: false, identity: true),
                        data_inicio = c.DateTime(nullable: false),
                        data_fim = c.DateTime(nullable: false),
                        validado = c.Boolean(nullable: false),
                        devolvido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmprestimoID);
            
            CreateTable(
                "dbo.Tratamentos",
                c => new
                    {
                        TratamentoID = c.Int(nullable: false, identity: true),
                        NomeTratamento = c.String(),
                    })
                .PrimaryKey(t => t.TratamentoID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EmprestimoPecas",
                c => new
                    {
                        Emprestimo_EmprestimoID = c.Int(nullable: false),
                        Peca_PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emprestimo_EmprestimoID, t.Peca_PecaID })
                .ForeignKey("dbo.Emprestimos", t => t.Emprestimo_EmprestimoID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.Peca_PecaID, cascadeDelete: true)
                .Index(t => t.Emprestimo_EmprestimoID)
                .Index(t => t.Peca_PecaID);
            
            CreateTable(
                "dbo.TratamentosPecas",
                c => new
                    {
                        Tratamentos_TratamentoID = c.Int(nullable: false),
                        Peca_PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tratamentos_TratamentoID, t.Peca_PecaID })
                .ForeignKey("dbo.Tratamentos", t => t.Tratamentos_TratamentoID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.Peca_PecaID, cascadeDelete: true)
                .Index(t => t.Tratamentos_TratamentoID)
                .Index(t => t.Peca_PecaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TratamentosPecas", "Peca_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.TratamentosPecas", "Tratamentos_TratamentoID", "dbo.Tratamentos");
            DropForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos");
            DropIndex("dbo.TratamentosPecas", new[] { "Peca_PecaID" });
            DropIndex("dbo.TratamentosPecas", new[] { "Tratamentos_TratamentoID" });
            DropIndex("dbo.EmprestimoPecas", new[] { "Peca_PecaID" });
            DropIndex("dbo.EmprestimoPecas", new[] { "Emprestimo_EmprestimoID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.TratamentosPecas");
            DropTable("dbo.EmprestimoPecas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tratamentos");
            DropTable("dbo.Emprestimos");
            DropTable("dbo.Pecas");
        }
    }
}
