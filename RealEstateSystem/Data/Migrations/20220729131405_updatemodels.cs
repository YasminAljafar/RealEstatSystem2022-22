using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateSystem.Data.Migrations
{
    public partial class updatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsPuplish",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "MeterPrice",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Bathroom",
                table: "Commerials");

            migrationBuilder.DropColumn(
                name: "Buffet",
                table: "Commerials");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Commerials");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisings");

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "PropertyImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Advertisings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Advertisings");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "PropertyImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuplish",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MeterPrice",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Position",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Bathroom",
                table: "Commerials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Buffet",
                table: "Commerials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Commerials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Advertisings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Advertisings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Advertisings",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Advertisings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
