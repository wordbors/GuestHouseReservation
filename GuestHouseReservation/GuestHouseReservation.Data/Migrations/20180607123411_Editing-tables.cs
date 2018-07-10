using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GuestHouseReservation.Data.Migrations
{
    public partial class Editingtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RoomTypes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rooms",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
