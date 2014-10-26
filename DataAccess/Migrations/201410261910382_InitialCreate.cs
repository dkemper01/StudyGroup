namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExchangeRates",
                c => new {
                    Id = c.Guid(nullable: false, identity: true),
                    Timestamp = c.DateTime(nullable: false),
                    CurrencyId = c.String(maxLength: 3),
                    CurrencyName = c.String(),
                    Rate = c.Decimal(nullable: false, precision: 19, scale: 9),
            })
            .PrimaryKey(t => t.Id, clustered: false)
            .Index(t => t.Timestamp, clustered: true)
            .Index(t => t.CurrencyId);
        }
        
        public override void Down()
        {
            DropTable("dbo.ExchangeRates");
        }
    }
}
