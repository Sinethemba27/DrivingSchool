namespace LindaniDrivingSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.CarGroups",
                c => new
                    {
                        CarGroupId = c.Int(nullable: false, identity: true),
                        CarGroupType = c.String(),
                    })
                .PrimaryKey(t => t.CarGroupId);
            
            CreateTable(
                "dbo.CarMakes",
                c => new
                    {
                        CarMakeId = c.Int(nullable: false, identity: true),
                        CarMakeType = c.String(),
                        Image = c.Binary(),
                        ImageType = c.String(),
                    })
                .PrimaryKey(t => t.CarMakeId);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        CarModelId = c.Int(nullable: false, identity: true),
                        CarModelType = c.String(),
                    })
                .PrimaryKey(t => t.CarModelId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        CarModelId = c.Int(nullable: false),
                        CarMakeId = c.Int(nullable: false),
                        TransmissionId = c.Int(nullable: false),
                        FuelID = c.Int(nullable: false),
                        InsuranceId = c.Int(nullable: false),
                        CarGroupId = c.Int(nullable: false),
                        Cost_Per_Day = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KilometerRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        freeKiloMeters = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentMilage = c.Int(nullable: false),
                        numTimesBooked = c.Int(nullable: false),
                        Description = c.String(),
                        Image = c.Binary(),
                        ImageType = c.String(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.CarGroups", t => t.CarGroupId, cascadeDelete: true)
                .ForeignKey("dbo.CarMakes", t => t.CarMakeId, cascadeDelete: true)
                .ForeignKey("dbo.CarModels", t => t.CarModelId, cascadeDelete: true)
                .ForeignKey("dbo.Fuels", t => t.FuelID, cascadeDelete: true)
                .ForeignKey("dbo.Insurances", t => t.InsuranceId, cascadeDelete: true)
                .ForeignKey("dbo.Transmissions", t => t.TransmissionId, cascadeDelete: true)
                .Index(t => t.CarModelId)
                .Index(t => t.CarMakeId)
                .Index(t => t.TransmissionId)
                .Index(t => t.FuelID)
                .Index(t => t.InsuranceId)
                .Index(t => t.CarGroupId);
            
            CreateTable(
                "dbo.Fuels",
                c => new
                    {
                        FuelID = c.Int(nullable: false, identity: true),
                        FuelType = c.String(),
                    })
                .PrimaryKey(t => t.FuelID);
            
            CreateTable(
                "dbo.Insurances",
                c => new
                    {
                        InsuranceId = c.Int(nullable: false, identity: true),
                        InsuranceType = c.String(),
                        InsuranceFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InsuranceId);
            
            CreateTable(
                "dbo.Transmissions",
                c => new
                    {
                        TransmissionId = c.Int(nullable: false, identity: true),
                        TransmissionType = c.String(),
                    })
                .PrimaryKey(t => t.TransmissionId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Cars", "TransmissionId", "dbo.Transmissions");
            DropForeignKey("dbo.Cars", "InsuranceId", "dbo.Insurances");
            DropForeignKey("dbo.Cars", "FuelID", "dbo.Fuels");
            DropForeignKey("dbo.Cars", "CarModelId", "dbo.CarModels");
            DropForeignKey("dbo.Cars", "CarMakeId", "dbo.CarMakes");
            DropForeignKey("dbo.Cars", "CarGroupId", "dbo.CarGroups");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Cars", new[] { "CarGroupId" });
            DropIndex("dbo.Cars", new[] { "InsuranceId" });
            DropIndex("dbo.Cars", new[] { "FuelID" });
            DropIndex("dbo.Cars", new[] { "TransmissionId" });
            DropIndex("dbo.Cars", new[] { "CarMakeId" });
            DropIndex("dbo.Cars", new[] { "CarModelId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transmissions");
            DropTable("dbo.Insurances");
            DropTable("dbo.Fuels");
            DropTable("dbo.Cars");
            DropTable("dbo.CarModels");
            DropTable("dbo.CarMakes");
            DropTable("dbo.CarGroups");
            DropTable("dbo.Admins");
        }
    }
}
