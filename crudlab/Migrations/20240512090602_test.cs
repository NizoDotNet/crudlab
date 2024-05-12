using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crudlab.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Specializations_SpecializationId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SpecializationId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Teachers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SpecializationId",
                table: "Teachers",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Specializations_SpecializationId",
                table: "Teachers",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
