using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveDirectoryManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class Final_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentEmails",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApproveEmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentEmails", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_DocumentEmails_DbEmployees_EmpCode",
                        column: x => x.EmpCode,
                        principalTable: "DbEmployees",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentEmails_DbStatuses_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "DbStatuses",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentPhones",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ApproveEmpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPhones", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_DocumentPhones_DbEmployees_EmpCode",
                        column: x => x.EmpCode,
                        principalTable: "DbEmployees",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentPhones_DbStatuses_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "DbStatuses",
                        principalColumn: "StatusCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEmails_EmpCode",
                table: "DocumentEmails",
                column: "EmpCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEmails_StatusCode",
                table: "DocumentEmails",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPhones_EmpCode",
                table: "DocumentPhones",
                column: "EmpCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPhones_StatusCode",
                table: "DocumentPhones",
                column: "StatusCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentEmails");

            migrationBuilder.DropTable(
                name: "DocumentPhones");
        }
    }
}
