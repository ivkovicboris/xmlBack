using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACar.DAL.Migrations
{
    public partial class AdAdRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdAdRequests",
                columns: table => new
                {
                    AdId = table.Column<Guid>(nullable: false),
                    AdRequestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdAdRequests", x => new { x.AdId, x.AdRequestId });
                    table.ForeignKey(
                        name: "FK_AdAdRequests_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdAdRequests_AdRequests_AdRequestId",
                        column: x => x.AdRequestId,
                        principalTable: "AdRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdAdRequests_AdRequestId",
                table: "AdAdRequests",
                column: "AdRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdAdRequests");

            migrationBuilder.DropTable(
                name: "AdRequests");
        }
    }
}
