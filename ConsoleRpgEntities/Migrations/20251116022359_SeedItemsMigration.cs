using Microsoft.EntityFrameworkCore.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace ConsoleRpgEntities.Migrations
{
    public partial class SeedItemsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Migrations", "Scripts", "SeedItems.sql");
            string sql = File.ReadAllText(path);
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optional: you could truncate the table
            migrationBuilder.Sql("DELETE FROM Items;");
        }
    }
}