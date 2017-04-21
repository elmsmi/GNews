namespace GNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestID = c.Int(nullable: false, identity: true),
                        RequestText = c.String(nullable: false),
                        Resolved = c.Boolean(nullable: false),
                        Employee_EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestID)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID, cascadeDelete: true)
                .Index(t => t.Employee_EmployeeID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewID = c.Int(nullable: false, identity: true),
                        NewText = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Client_ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.NewID)
                .ForeignKey("dbo.Clients", t => t.Client_ClientID)
                .Index(t => t.Client_ClientID);
            
            CreateTable(
                "dbo.EmployeeClients",
                c => new
                    {
                        Employee_EmployeeID = c.Int(nullable: false),
                        Client_ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Employee_EmployeeID, t.Client_ClientID })
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_ClientID, cascadeDelete: true)
                .Index(t => t.Employee_EmployeeID)
                .Index(t => t.Client_ClientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Client_ClientID", "dbo.Clients");
            DropForeignKey("dbo.Requests", "Employee_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeClients", "Client_ClientID", "dbo.Clients");
            DropForeignKey("dbo.EmployeeClients", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeClients", new[] { "Client_ClientID" });
            DropIndex("dbo.EmployeeClients", new[] { "Employee_EmployeeID" });
            DropIndex("dbo.News", new[] { "Client_ClientID" });
            DropIndex("dbo.Requests", new[] { "Employee_EmployeeID" });
            DropTable("dbo.EmployeeClients");
            DropTable("dbo.News");
            DropTable("dbo.Requests");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
        }
    }
}
