namespace GSMovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteFlagToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsDeleted");
        }
    }
}
