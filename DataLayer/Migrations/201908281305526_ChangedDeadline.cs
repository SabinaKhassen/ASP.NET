namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDeadline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBookLinks", "Deadline", c => c.Int(nullable: false));
            DropColumn("dbo.UserBookLinks", "DeadlineDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBookLinks", "DeadlineDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserBookLinks", "Deadline");
        }
    }
}
