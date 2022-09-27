using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domashna1.Repository.Migrations
{
    public partial class addedClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TicketCarts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    OrderedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_OrderedById",
                        column: x => x.OrderedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketsInTicketCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false),
                    CartId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsInTicketCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketsInTicketCarts_Tickets_CartId",
                        column: x => x.CartId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketsInTicketCarts_TicketCarts_TicketId",
                        column: x => x.TicketId,
                        principalTable: "TicketCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketsInOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsInOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketsInOrders_Tickets_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketsInOrders_Orders_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketCarts_ApplicationUserId",
                table: "TicketCarts",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedById",
                table: "Orders",
                column: "OrderedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrders_OrderId",
                table: "TicketsInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInOrders_TicketId",
                table: "TicketsInOrders",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInTicketCarts_CartId",
                table: "TicketsInTicketCarts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsInTicketCarts_TicketId",
                table: "TicketsInTicketCarts",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCarts_AspNetUsers_ApplicationUserId",
                table: "TicketCarts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketCarts_AspNetUsers_ApplicationUserId",
                table: "TicketCarts");

            migrationBuilder.DropTable(
                name: "TicketsInOrders");

            migrationBuilder.DropTable(
                name: "TicketsInTicketCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_TicketCarts_ApplicationUserId",
                table: "TicketCarts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TicketCarts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
