using ConsoleRpgEntities.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract partial class BaseMigration : Migration
{
    protected void RunSql(MigrationBuilder migrationBuilder)
    {
        string migrationName = GetType().Name;
        string sql = MigrationHelper.GetMigrationScript(migrationName, "Up");
        migrationBuilder.Sql(sql);
    }

    protected void RunSqlRollback(MigrationBuilder migrationBuilder)
    {
        string migrationName = GetType().Name;
        string sql = MigrationHelper.GetMigrationScript(migrationName, "Down");
        migrationBuilder.Sql(sql);
    }
}
