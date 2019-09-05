namespace DataLayer.Migrations
{ 
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatesLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBookLinks", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserBookLinks", "DeadlineDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserBookLinks", "ReturnDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserBookLinks", "ReturnDate");
            DropColumn("dbo.UserBookLinks", "DeadlineDate");
            DropColumn("dbo.UserBookLinks", "CreationDate");
        }
    }
}
