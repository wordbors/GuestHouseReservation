using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GuestHouseReservation.Data.Migrations
{
    public partial class deleterelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_House_Extras_ExtraID",
                table: "House");

            migrationBuilder.DropIndex(
                name: "IX_House_ExtraID",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ExtraID",
                table: "House");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtraID",
                table: "House",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_House_ExtraID",
                table: "House",
                column: "ExtraID");

            migrationBuilder.AddForeignKey(
                name: "FK_House_Extras_ExtraID",
                table: "House",
                column: "ExtraID",
                principalTable: "Extras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
