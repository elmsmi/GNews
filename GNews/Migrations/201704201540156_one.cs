namespace GNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "RequestText", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "RequestText", c => c.String(nullable: false));
        }
    }
}
