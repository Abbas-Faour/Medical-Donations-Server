using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Donations_Server.Infrastructure.Data.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("INSERT INTO Categories VALUES('Request')");
            migrationBuilder.Sql("INSERT INTO Categories VALUES('Offer')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
