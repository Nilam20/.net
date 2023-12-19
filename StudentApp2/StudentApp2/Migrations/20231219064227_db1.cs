using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentApp2.Migrations
{
    public partial class db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentDatas",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Standard = table.Column<int>(type: "int", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RollNo = table.Column<int>(type: "int", nullable: false),
                    GRNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDatas", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjectMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectMarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectDetails",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    MaxMark = table.Column<int>(type: "int", nullable: false),
                    MinMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDetails", x => x.SubjectId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentDatas");

            migrationBuilder.DropTable(
                name: "StudentSubjectMarks");

            migrationBuilder.DropTable(
                name: "StudentSubjects1");

            migrationBuilder.DropTable(
                name: "SubjectDetails");
        }
    }
}
