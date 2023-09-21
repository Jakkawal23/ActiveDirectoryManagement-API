using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveDirectoryManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbDepartments",
                columns: table => new
                {
                    DepartmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentNameTH = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DepartmentNameEN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbDepartments", x => x.DepartmentCode);
                });

            migrationBuilder.CreateTable(
                name: "DbPositions",
                columns: table => new
                {
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionNameTH = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PositionNameEN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPositions", x => x.PositionCode);
                });

            migrationBuilder.CreateTable(
                name: "DbStatuses",
                columns: table => new
                {
                    StatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusNameTH = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusNameEN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbStatuses", x => x.StatusCode);
                });

            migrationBuilder.CreateTable(
                name: "DbTeams",
                columns: table => new
                {
                    TeamCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamNameTH = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TeamNameEN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTeams", x => x.TeamCode);
                });

            migrationBuilder.CreateTable(
                name: "SuProfiles",
                columns: table => new
                {
                    ProfileCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileNameTH = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProfileNameEN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuProfiles", x => x.ProfileCode);
                });

            migrationBuilder.CreateTable(
                name: "DbEmployees",
                columns: table => new
                {
                    EmployeeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TeamCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbEmployees", x => x.EmployeeCode);
                    table.ForeignKey(
                        name: "FK_DbEmployees_DbDepartments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "DbDepartments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbEmployees_DbPositions_PositionCode",
                        column: x => x.PositionCode,
                        principalTable: "DbPositions",
                        principalColumn: "PositionCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbEmployees_DbTeams_TeamCode",
                        column: x => x.TeamCode,
                        principalTable: "DbTeams",
                        principalColumn: "TeamCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentPasswords",
                columns: table => new
                {
                    PasswordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApproveEmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPasswords", x => x.PasswordId);
                    table.ForeignKey(
                        name: "FK_DocumentPasswords_DbEmployees_EmpCode",
                        column: x => x.EmpCode,
                        principalTable: "DbEmployees",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentPasswords_DbStatuses_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "DbStatuses",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MobilePhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProfileCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_SuUsers_DbEmployees_EmpCode",
                        column: x => x.EmpCode,
                        principalTable: "DbEmployees",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuUsers_SuProfiles_ProfileCode",
                        column: x => x.ProfileCode,
                        principalTable: "SuProfiles",
                        principalColumn: "ProfileCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployees_DepartmentCode",
                table: "DbEmployees",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployees_PositionCode",
                table: "DbEmployees",
                column: "PositionCode");

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployees_TeamCode",
                table: "DbEmployees",
                column: "TeamCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPasswords_EmpCode",
                table: "DocumentPasswords",
                column: "EmpCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPasswords_StatusCode",
                table: "DocumentPasswords",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_SuUsers_EmpCode",
                table: "SuUsers",
                column: "EmpCode");

            migrationBuilder.CreateIndex(
                name: "IX_SuUsers_ProfileCode",
                table: "SuUsers",
                column: "ProfileCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentPasswords");

            migrationBuilder.DropTable(
                name: "SuUsers");

            migrationBuilder.DropTable(
                name: "DbStatuses");

            migrationBuilder.DropTable(
                name: "DbEmployees");

            migrationBuilder.DropTable(
                name: "SuProfiles");

            migrationBuilder.DropTable(
                name: "DbDepartments");

            migrationBuilder.DropTable(
                name: "DbPositions");

            migrationBuilder.DropTable(
                name: "DbTeams");
        }
    }
}
