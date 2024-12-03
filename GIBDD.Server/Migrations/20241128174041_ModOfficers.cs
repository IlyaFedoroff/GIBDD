using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIBDD.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModOfficers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Officers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Officers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Officers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ManufactureDate",
                table: "Cars",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Officers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Officers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Officers",
                newName: "FullName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ManufactureDate",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
