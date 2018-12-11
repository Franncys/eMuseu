namespace eMuseu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos");
            DropForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas");
            DropIndex("dbo.EmprestimoPecas", new[] { "Emprestimo_EmprestimoID" });
            DropIndex("dbo.EmprestimoPecas", new[] { "Peca_PecaID" });
            CreateTable(
                "dbo.Emprestimos_Pecas",
                c => new
                    {
                        EmprestimoID = c.Int(nullable: false),
                        PecaID = c.Int(nullable: false),
                        deterioracao = c.String(),
                    })
                .PrimaryKey(t => new { t.EmprestimoID, t.PecaID })
                .ForeignKey("dbo.Emprestimos", t => t.PecaID, cascadeDelete: true)
                .ForeignKey("dbo.Pecas", t => t.PecaID, cascadeDelete: true)
                .Index(t => t.PecaID);
            
            CreateTable(
                "dbo.Rececoes",
                c => new
                    {
                        rececaoID = c.Int(nullable: false, identity: true),
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
            
            DropTable("dbo.Utilizadores");
            DropTable("dbo.EmprestimoPecas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmprestimoPecas",
                c => new
                    {
                        Emprestimo_EmprestimoID = c.Int(nullable: false),
                        Peca_PecaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emprestimo_EmprestimoID, t.Peca_PecaID });
            
            CreateTable(
                "dbo.Utilizadores",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        UserTipo = c.Int(nullable: false),
                        Morada = c.String(nullable: false),
                        Cidade = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        aprovado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rececoes", "PecaID_PecaID", "dbo.Pecas");
            DropForeignKey("dbo.Emprestimos_Pecas", "PecaID", "dbo.Pecas");
            DropForeignKey("dbo.Emprestimos_Pecas", "PecaID", "dbo.Emprestimos");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rececoes", new[] { "PecaID_PecaID" });
            DropIndex("dbo.Emprestimos_Pecas", new[] { "PecaID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rececoes");
            DropTable("dbo.Emprestimos_Pecas");
            CreateIndex("dbo.EmprestimoPecas", "Peca_PecaID");
            CreateIndex("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID");
            AddForeignKey("dbo.EmprestimoPecas", "Peca_PecaID", "dbo.Pecas", "PecaID", cascadeDelete: true);
            AddForeignKey("dbo.EmprestimoPecas", "Emprestimo_EmprestimoID", "dbo.Emprestimos", "EmprestimoID", cascadeDelete: true);
        }
    }
}
