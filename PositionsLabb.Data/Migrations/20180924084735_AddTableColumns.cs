using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PositionsLabb.Data.Migrations
{
    public partial class AddTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "Vehicles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Positions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Positions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Vehicles_VehicleId",
                table: "Positions",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Vehicles_VehicleId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Positions");
        }
    }
}
