using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CultureU.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InscripcionDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumno", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Directivas",
                columns: table => new
                {
                    ProfesorID = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directivas", x => x.ProfesorID);
                    table.ForeignKey(
                        name: "FK_Directivas_Profesor_ProfesorID",
                        column: x => x.ProfesorID,
                        principalTable: "Profesor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escuelas",
                columns: table => new
                {
                    EscuelaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProfesorID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuelas", x => x.EscuelaID);
                    table.ForeignKey(
                        name: "FK_Escuelas_Profesor_ProfesorID",
                        column: x => x.ProfesorID,
                        principalTable: "Profesor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    MateriaID = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    EscuelaID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.MateriaID);
                    table.ForeignKey(
                        name: "FK_Materia_Escuelas_EscuelaID",
                        column: x => x.EscuelaID,
                        principalTable: "Escuelas",
                        principalColumn: "EscuelaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    InscripcionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MateriaID = table.Column<int>(type: "INTEGER", nullable: false),
                    AlumnoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.InscripcionID);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Alumno_AlumnoID",
                        column: x => x.AlumnoID,
                        principalTable: "Alumno",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Materia_MateriaID",
                        column: x => x.MateriaID,
                        principalTable: "Materia",
                        principalColumn: "MateriaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriaProfesor",
                columns: table => new
                {
                    MateriasMateriaID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfesoresID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaProfesor", x => new { x.MateriasMateriaID, x.ProfesoresID });
                    table.ForeignKey(
                        name: "FK_MateriaProfesor_Materia_MateriasMateriaID",
                        column: x => x.MateriasMateriaID,
                        principalTable: "Materia",
                        principalColumn: "MateriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriaProfesor_Profesor_ProfesoresID",
                        column: x => x.ProfesoresID,
                        principalTable: "Profesor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_ProfesorID",
                table: "Escuelas",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_AlumnoID",
                table: "Inscripciones",
                column: "AlumnoID");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_MateriaID",
                table: "Inscripciones",
                column: "MateriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_EscuelaID",
                table: "Materia",
                column: "EscuelaID");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaProfesor_ProfesoresID",
                table: "MateriaProfesor",
                column: "ProfesoresID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Directivas");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "MateriaProfesor");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Escuelas");

            migrationBuilder.DropTable(
                name: "Profesor");
        }
    }
}
