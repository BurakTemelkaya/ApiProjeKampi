using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeKampi.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskChef_Chefs_ChefId",
                table: "EmployeeTaskChef");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskChef_EmployeeTasks_EmployeeTaskId",
                table: "EmployeeTaskChef");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTaskChef",
                table: "EmployeeTaskChef");

            migrationBuilder.RenameTable(
                name: "EmployeeTaskChef",
                newName: "EmployeeTaskChefs");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTaskChef_EmployeeTaskId",
                table: "EmployeeTaskChefs",
                newName: "IX_EmployeeTaskChefs_EmployeeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTaskChef_ChefId",
                table: "EmployeeTaskChefs",
                newName: "IX_EmployeeTaskChefs_ChefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTaskChefs",
                table: "EmployeeTaskChefs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GroupReservations",
                columns: table => new
                {
                    GroupReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupReservations", x => x.GroupReservationId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskChefs_Chefs_ChefId",
                table: "EmployeeTaskChefs",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskChefs_EmployeeTasks_EmployeeTaskId",
                table: "EmployeeTaskChefs",
                column: "EmployeeTaskId",
                principalTable: "EmployeeTasks",
                principalColumn: "EmployeeTaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskChefs_Chefs_ChefId",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskChefs_EmployeeTasks_EmployeeTaskId",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropTable(
                name: "GroupReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTaskChefs",
                table: "EmployeeTaskChefs");

            migrationBuilder.RenameTable(
                name: "EmployeeTaskChefs",
                newName: "EmployeeTaskChef");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTaskChefs_EmployeeTaskId",
                table: "EmployeeTaskChef",
                newName: "IX_EmployeeTaskChef_EmployeeTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeTaskChefs_ChefId",
                table: "EmployeeTaskChef",
                newName: "IX_EmployeeTaskChef_ChefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTaskChef",
                table: "EmployeeTaskChef",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskChef_Chefs_ChefId",
                table: "EmployeeTaskChef",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskChef_EmployeeTasks_EmployeeTaskId",
                table: "EmployeeTaskChef",
                column: "EmployeeTaskId",
                principalTable: "EmployeeTasks",
                principalColumn: "EmployeeTaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
