using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class TimestampRevised : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Users",
                type: "datetime2",
                rowVersion: true,
                nullable: true,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldRowVersion: true,
                oldNullable: true,
                oldComputedColumnSql: "GetUtcDate()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Users",
                type: "datetime2",
                rowVersion: true,
                nullable: true,
                computedColumnSql: "GetUtcDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldRowVersion: true,
                oldNullable: true,
                oldDefaultValueSql: "GetDate()");
        }
    }
}
