using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementInCore.Data.Migrations
{
    public partial class AssignPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerViewModelBaseTrainerID",
                table: "TrainerExperiences",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainerViewModelBase",
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
                    table.PrimaryKey("PK_TrainerViewModelBase", x => x.TrainerID);
                    table.ForeignKey(
                        name: "FK_TrainerViewModelBase_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerExperiences_TrainerViewModelBaseTrainerID",
                table: "TrainerExperiences",
                column: "TrainerViewModelBaseTrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerViewModelBase_CourseID",
                table: "TrainerViewModelBase",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerExperiences_TrainerViewModelBase_TrainerViewModelBaseTrainerID",
                table: "TrainerExperiences",
                column: "TrainerViewModelBaseTrainerID",
                principalTable: "TrainerViewModelBase",
                principalColumn: "TrainerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerExperiences_TrainerViewModelBase_TrainerViewModelBaseTrainerID",
                table: "TrainerExperiences");

            migrationBuilder.DropTable(
                name: "TrainerViewModelBase");

            migrationBuilder.DropIndex(
                name: "IX_TrainerExperiences_TrainerViewModelBaseTrainerID",
                table: "TrainerExperiences");

            migrationBuilder.DropColumn(
                name: "TrainerViewModelBaseTrainerID",
                table: "TrainerExperiences");
        }
    }
}
