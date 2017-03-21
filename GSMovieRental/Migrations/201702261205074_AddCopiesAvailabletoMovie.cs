namespace GSMovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCopiesAvailabletoMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "CopiesAvailable", c => c.Short(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(maxLength: 255));
            DropColumn("dbo.Movies", "CopiesAvailable");
        }
    }
}
