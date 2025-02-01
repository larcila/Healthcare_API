using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class _25012025_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Registration_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Registration_User_ID = table.Column<int>(type: "int", nullable: false),
                    Update_User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Registration_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Registration_User_ID = table.Column<int>(type: "int", nullable: false),
                    Update_User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Follow_Ups",
                columns: table => new
                {
                    Follow_Up_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allergy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symptom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screening = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patient_ID = table.Column<int>(type: "int", nullable: false),
                    Registration_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modify_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Registration_User_ID = table.Column<int>(type: "int", nullable: false),
                    Update_User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow_Ups", x => x.Follow_Up_ID);
                    table.ForeignKey(
                        name: "FK_Follow_Ups_Patients_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles_By_Users",
                columns: table => new
                {
                    Role_By_User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_By_Users", x => x.Role_By_User_Id);
                    table.ForeignKey(
                        name: "FK_Roles_By_Users_Roles_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "Role_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_By_Users_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Role_ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "the user has access to the entire system", "ADMIN" },
                    { 2, "user has limited access", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follow_Ups_Patient_ID",
                table: "Follow_Ups",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_By_Users_Role_ID",
                table: "Roles_By_Users",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_By_Users_User_ID_Role_ID",
                table: "Roles_By_Users",
                columns: new[] { "User_ID", "Role_ID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follow_Ups");

            migrationBuilder.DropTable(
                name: "Roles_By_Users");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
