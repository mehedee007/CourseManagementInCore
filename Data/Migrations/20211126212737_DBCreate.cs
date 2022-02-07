using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementInCore.Data.Migrations
{
    public partial class DBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(nullable: true),
                    CourseDuration = table.Column<string>(nullable: true),
                    CourseFee = table.Column<int>(nullable: true),
                    FeeWithVat = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    TraineeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeName = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    TraineeEmail = table.Column<string>(nullable: true),
                    TraineeContact = table.Column<string>(maxLength: 11, nullable: true),
                    CourseID = table.Column<int>(nullable: false),
                    TraineeImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeID);
                    table.ForeignKey(
                        name: "FK_Trainees_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    TrainerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    TrainerContact = table.Column<string>(nullable: true),
                    TrainerEmail = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    TrainerImage = table.Column<string>(nullable: true),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.TrainerID);
                    table.ForeignKey(
                        name: "FK_Trainers_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraineeModuleDescriptions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(nullable: true),
                    TraineeID = table.Column<int>(nullable: false),
                    ExternalMark = table.Column<int>(nullable: false),
                    ExternalDate = table.Column<DateTime>(nullable: false),
                    EvidenceMark = table.Column<int>(nullable: false),
                    EvidenceDate = table.Column<DateTime>(nullable: false),
                    IsPass = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeModuleDescriptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TraineeModuleDescriptions_Trainees_TraineeID",
                        column: x => x.TraineeID,
                        principalTable: "Trainees",
                        principalColumn: "TraineeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerExperiences",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(nullable: true),
                    TrainerID = table.Column<int>(nullable: false),
                    Institution = table.Column<string>(nullable: true),
                    ExperienceInYears = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerExperiences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainerExperiences_Trainers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "Trainers",
                        principalColumn: "TrainerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TSPs",
                columns: table => new
                {
                    TspID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TspName = table.Column<string>(nullable: true),
                    TspAddress = table.Column<string>(nullable: true),
                    TspContact = table.Column<string>(nullable: true),
                    TspEmail = table.Column<string>(nullable: true),
                    TrainerID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSPs", x => x.TspID);
                    table.ForeignKey(
                        name: "FK_TSPs_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TSPs_Trainers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "Trainers",
                        principalColumn: "TrainerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraineeModuleDescriptions_TraineeID",
                table: "TraineeModuleDescriptions",
                column: "TraineeID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_CourseID",
                table: "Trainees",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerExperiences_TrainerID",
                table: "TrainerExperiences",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_CourseID",
                table: "Trainers",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TSPs_CourseID",
                table: "TSPs",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TSPs_TrainerID",
                table: "TSPs",
                column: "TrainerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraineeModuleDescriptions");

            migrationBuilder.DropTable(
                name: "TrainerExperiences");

            migrationBuilder.DropTable(
                name: "TSPs");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
