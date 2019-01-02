namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalDBV6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emp_Peca",
                c => new
                    {
                        PecaID = c.Int(nullable: false),
                        EmprestimoID = c.Int(nullable: false),
                        Estado = c.String(),
                        data_Entregue = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.PecaID, t.EmprestimoID })
                .ForeignKey("dbo.Emprestimos", t => t.EmprestimoID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.PecaID, cascadeDelete: true)
                .Index(t => t.PecaID)
                .Index(t => t.EmprestimoID);
            
            CreateTable(
                "dbo.Emprestimos",
                c => new
                    {
                        EmprestimoID = c.Int(nullable: false, identity: true),
                        data_inicio = c.DateTime(),
                        data_fim = c.DateTime(nullable: false),
                        validado = c.Boolean(nullable: false),
                        devolvido = c.Boolean(nullable: false),
                        userID = c.String(),
                    })
                .PrimaryKey(t => t.EmprestimoID);
            
            CreateTable(
                "dbo.Pecas",
                c => new
                    {
                        PecaID = c.Int(nullable: false, identity: true),
                        nomePeca = c.String(),
                        Periodo = c.Int(nullable: false),
                        Zona = c.String(),
                        Estado = c.String(),
                        PecaTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PecaID);
            
            CreateTable(
                "dbo.Mensagem",
                c => new
                    {
                        MensagemID = c.Int(nullable: false, identity: true),
                        OrigemID = c.String(),
                        DestinoID = c.String(),
                        EmailDest = c.String(nullable: false),
                        EmailOri = c.String(),
                        Msg = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MensagemID);
            
            CreateTable(
                "dbo.Rececoes",
                c => new
                    {
                        rececaoID = c.Int(nullable: false, identity: true),
                        formulario = c.String(),
                        antes = c.String(),
                        depois = c.String(),
                        cumprimento = c.Int(nullable: false),
                        PecaID_PecaID = c.Int(),
                    })
                .PrimaryKey(t => t.rececaoID)
                .ForeignKey("dbo.Pecas", t => t.PecaID_PecaID)
                .Index(t => t.PecaID_PecaID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                "dbo.Tratamentos",
                c => new
                    {
                        TratamentoID = c.Int(nullable: false, identity: true),
                        NomeTratamento = c.String(),
                        PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TratamentoID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NomeP = c.String(),
                        NomeU = c.String(),
                        DataNascimento = c.DateTime(),
                        Cidade = c.String(),
                        Morada = c.String(),
                        aprovado = c.Boolean(nullable: false),
                        RoleName = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rececoes", "PecaID_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.Emp_Peca", "PecaID", "dbo.Pecas");
            DropForeignKey("dbo.Emp_Peca", "EmprestimoID", "dbo.Emprestimos");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rececoes", new[] { "PecaID_PecaID" });
            DropIndex("dbo.Emp_Peca", new[] { "EmprestimoID" });
            DropIndex("dbo.Emp_Peca", new[] { "PecaID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tratamentos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rececoes");
            DropTable("dbo.Mensagem");
            DropTable("dbo.Pecas");
            DropTable("dbo.Emprestimos");
            DropTable("dbo.Emp_Peca");
        }
    }
}
