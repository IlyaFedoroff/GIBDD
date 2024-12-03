using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIBDD.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedNamingForOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Owners",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Owners",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Owners",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ManufactureDate",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Owners",
                newName: "FullName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ManufactureDate",
                table: "Cars",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
