using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio4Tech.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPrioridade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Prioridade",
                table: "Planos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 5,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Prioridade",
                table: "Planos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 5);
        }
    }
}
