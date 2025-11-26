using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio4Tech.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddCodigoRegistroAns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Codigo_registro_ans",
                table: "Planos",
                newName: "CodigoRegistroAns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoRegistroAns",
                table: "Planos",
                newName: "Codigo_registro_ans");
        }
    }
}
