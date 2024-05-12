using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crudlab.Migrations
{
    /// <inheritdoc />
    public partial class teachersubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecializationTeacher");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    SubjectsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.SubjectsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SpecializationId",
                table: "Teachers",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeachersId",
                table: "SubjectTeacher",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Specializations_SpecializationId",
                table: "Teachers",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Specializations_SpecializationId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SpecializationId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Teachers");

            migrationBuilder.CreateTable(
                name: "SpecializationTeacher",
                columns: table => new
                {
                    SpecializationsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecializationTeacher", x => new { x.SpecializationsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SpecializationTeacher_Specializations_SpecializationsId",
                        column: x => x.SpecializationsId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecializationTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializationTeacher_TeachersId",
                table: "SpecializationTeacher",
                column: "TeachersId");
        }
    }
}
