using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio4Tech.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDataExclusao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Beneficiarios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Beneficiarios");
        }
    }
}
