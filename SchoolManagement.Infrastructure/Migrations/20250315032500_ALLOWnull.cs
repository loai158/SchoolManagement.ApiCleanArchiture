using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ALLOWnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManger",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "InsManger",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManger",
                table: "Departments",
                column: "InsManger",
                unique: true,
                filter: "[InsManger] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManger",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "InsManger",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManger",
                table: "Departments",
                column: "InsManger",
                unique: true);
        }
    }
}
