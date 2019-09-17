namespace JNPPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 200),
                        AddressTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddressType", t => t.AddressTypeId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.AddressTypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        DateOfBirth = c.DateTime(),
                        ProfilePicture = c.String(maxLength: 100),
                        Email = c.String(maxLength: 50),
                        EmailSignature = c.String(maxLength: 500),
                        About = c.String(maxLength: 1000),
                        Website = c.String(maxLength: 100),
                        GenderId = c.Int(nullable: false),
                        MaritalStatusId = c.Int(nullable: false),
                        Phone = c.String(maxLength: 15),
                        IsActive = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                        Dated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gender", t => t.GenderId)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId)
                .ForeignKey("dbo.User", t => t.ParentId)
                .Index(t => t.GenderId)
                .Index(t => t.MaritalStatusId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Gender",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuPermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(),
                        SortOrder = c.Int(),
                        IsCreate = c.Boolean(),
                        IsRead = c.Boolean(),
                        IsUpdate = c.Boolean(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.MenuId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuText = c.String(nullable: false, maxLength: 100),
                        MenuURL = c.String(nullable: false, maxLength: 400),
                        ParentId = c.Int(),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserEmail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMapLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Latitude = c.String(nullable: false, maxLength: 50),
                        Longitude = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPhone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserQualification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Qualification = c.String(nullable: false, maxLength: 100),
                        FromYear = c.String(maxLength: 10),
                        ToYear = c.String(maxLength: 10),
                        BoardUniversity = c.String(maxLength: 50),
                        TotalMarks = c.String(nullable: false, maxLength: 50),
                        OutOfMarks = c.String(maxLength: 50),
                        Percentage = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SettingKey = c.String(nullable: false, maxLength: 100),
                        SettingValue = c.String(nullable: false, maxLength: 500),
                        SettingGroup = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSkill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SkillName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserWorkExperience",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 100),
                        RoleName = c.String(nullable: false, maxLength: 100),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        JobDescription = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CountryCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.State", "CountryId", "dbo.Country");
            DropForeignKey("dbo.UserAddress", "UserId", "dbo.User");
            DropForeignKey("dbo.UserWorkExperience", "UserId", "dbo.User");
            DropForeignKey("dbo.UserSkill", "UserId", "dbo.User");
            DropForeignKey("dbo.UserSetting", "UserId", "dbo.User");
            DropForeignKey("dbo.UserQualification", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPhone", "UserId", "dbo.User");
            DropForeignKey("dbo.UserMapLocation", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLanguage", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLanguage", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.UserEmail", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "ParentId", "dbo.User");
            DropForeignKey("dbo.MenuPermission", "UserId", "dbo.User");
            DropForeignKey("dbo.MenuPermission", "RoleId", "dbo.Role");
            DropForeignKey("dbo.RoleUser", "UserId", "dbo.User");
            DropForeignKey("dbo.RoleUser", "RoleId", "dbo.Role");
            DropForeignKey("dbo.MenuPermission", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.Menu", "ParentId", "dbo.Menu");
            DropForeignKey("dbo.User", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.User", "GenderId", "dbo.Gender");
            DropForeignKey("dbo.UserAddress", "AddressTypeId", "dbo.AddressType");
            DropIndex("dbo.State", new[] { "CountryId" });
            DropIndex("dbo.UserAddress", new[] { "UserId" });
            DropIndex("dbo.UserWorkExperience", new[] { "UserId" });
            DropIndex("dbo.UserSkill", new[] { "UserId" });
            DropIndex("dbo.UserSetting", new[] { "UserId" });
            DropIndex("dbo.UserQualification", new[] { "UserId" });
            DropIndex("dbo.UserPhone", new[] { "UserId" });
            DropIndex("dbo.UserMapLocation", new[] { "UserId" });
            DropIndex("dbo.UserLanguage", new[] { "UserId" });
            DropIndex("dbo.UserLanguage", new[] { "LanguageId" });
            DropIndex("dbo.UserEmail", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "ParentId" });
            DropIndex("dbo.MenuPermission", new[] { "UserId" });
            DropIndex("dbo.MenuPermission", new[] { "RoleId" });
            DropIndex("dbo.RoleUser", new[] { "UserId" });
            DropIndex("dbo.RoleUser", new[] { "RoleId" });
            DropIndex("dbo.MenuPermission", new[] { "MenuId" });
            DropIndex("dbo.Menu", new[] { "ParentId" });
            DropIndex("dbo.User", new[] { "MaritalStatusId" });
            DropIndex("dbo.User", new[] { "GenderId" });
            DropIndex("dbo.UserAddress", new[] { "AddressTypeId" });
            DropTable("dbo.State");
            DropTable("dbo.Country");
            DropTable("dbo.UserWorkExperience");
            DropTable("dbo.UserSkill");
            DropTable("dbo.UserSetting");
            DropTable("dbo.UserQualification");
            DropTable("dbo.UserPhone");
            DropTable("dbo.UserMapLocation");
            DropTable("dbo.Language");
            DropTable("dbo.UserLanguage");
            DropTable("dbo.UserEmail");
            DropTable("dbo.RoleUser");
            DropTable("dbo.Role");
            DropTable("dbo.Menu");
            DropTable("dbo.MenuPermission");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Gender");
            DropTable("dbo.User");
            DropTable("dbo.UserAddress");
            DropTable("dbo.AddressType");
        }
    }
}
