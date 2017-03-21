namespace GSMovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        ReleaseDate = c.DateTime(nullable: false),
                        Genre = c.String(nullable: false, maxLength: 30),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
        }
    }
}
