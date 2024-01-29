using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProjectASPnetMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Фамилия = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Отчество = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Образование = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Специальность = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Дата_Рождения = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Телефон = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Фото = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Фамилия = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Отчество = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Организация = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Дата_Основания = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Вакансия = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Телефон = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Фото = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "Employer");
        }
    }
}
