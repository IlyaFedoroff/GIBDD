using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIBDD.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBirthYearToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Преобразование столбца с указанием явного преобразования данных
            migrationBuilder.Sql(
                "ALTER TABLE \"Owners\" ALTER COLUMN \"BirthYear\" TYPE TIMESTAMP WITH TIME ZONE USING TO_TIMESTAMP(\"BirthYear\" || '-01-01', 'YYYY-MM-DD')"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Обратное изменение типа, если потребуется откат
            migrationBuilder.Sql(
                "ALTER TABLE \"Owners\" ALTER COLUMN \"BirthYear\" TYPE INTEGER USING EXTRACT(YEAR FROM \"BirthYear\")::INTEGER"
            );
        }
    }
}
