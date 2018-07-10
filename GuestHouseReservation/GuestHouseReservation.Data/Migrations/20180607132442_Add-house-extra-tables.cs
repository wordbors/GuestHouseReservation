using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GuestHouseReservation.Data.Migrations
{
    public partial class Addhouseextratables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RoomTypes",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Discription = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ExtraID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.ID);
                    table.ForeignKey(
                        name: "FK_House_Extras_ExtraID",
                        column: x => x.ExtraID,
                        principalTable: "Extras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_House_ExtraID",
                table: "House",
                column: "ExtraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RoomTypes",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
