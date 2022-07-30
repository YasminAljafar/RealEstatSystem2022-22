using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateSystem.Data.Migrations
{
    public partial class changeUserIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_AspNetUsers_UserId",
                table: "Advertisings");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUsers_AspNetUsers_ApplicationUserId1",
                table: "PropertyUsers");

            migrationBuilder.DropIndex(
                name: "IX_PropertyUsers_ApplicationUserId1",
                table: "PropertyUsers");

            migrationBuilder.DropIndex(
                name: "IX_Advertisings_UserId",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "PropertyUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Advertisings");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserPreferences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PropertyUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Advertisings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUsers_ApplicationUserId",
                table: "PropertyUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_ApplicationUserId",
                table: "Advertisings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_AspNetUsers_ApplicationUserId",
                table: "Advertisings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUsers_AspNetUsers_ApplicationUserId",
                table: "PropertyUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_AspNetUsers_ApplicationUserId",
                table: "Advertisings");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUsers_AspNetUsers_ApplicationUserId",
                table: "PropertyUsers");

            migrationBuilder.DropIndex(
                name: "IX_PropertyUsers_ApplicationUserId",
                table: "PropertyUsers");

            migrationBuilder.DropIndex(
                name: "IX_Advertisings_ApplicationUserId",
                table: "Advertisings");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "UserPreferences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "PropertyUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "PropertyUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Advertisings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Advertisings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUsers_ApplicationUserId1",
                table: "PropertyUsers",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_UserId",
                table: "Advertisings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_AspNetUsers_UserId",
                table: "Advertisings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUsers_AspNetUsers_ApplicationUserId1",
                table: "PropertyUsers",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
