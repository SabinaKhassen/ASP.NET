namespace ASP.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserBookLinks", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserBookLinks", "Deadline", c => c.Int(nullable: false));
        }
    }
}
