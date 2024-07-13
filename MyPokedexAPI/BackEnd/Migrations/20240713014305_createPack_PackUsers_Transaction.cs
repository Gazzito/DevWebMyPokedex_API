using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyPokedexAPI.Migrations
{
    /// <inheritdoc />
    public partial class createPack_PackUsers_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    BronzeChance = table.Column<double>(type: "double precision", nullable: false),
                    SilverChance = table.Column<double>(type: "double precision", nullable: false),
                    GoldChance = table.Column<double>(type: "double precision", nullable: false),
                    PlatinumChance = table.Column<double>(type: "double precision", nullable: false),
                    DiamondChance = table.Column<double>(type: "double precision", nullable: false),
                    TotalBought = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pack_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pack_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PackId = table.Column<int>(type: "integer", nullable: false),
                    OpenedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NextOpenExpected = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackUsers_Pack_PackId",
                        column: x => x.PackId,
                        principalTable: "Pack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PackId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Pack_PackId",
                        column: x => x.PackId,
                        principalTable: "Pack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pack_CreatedById",
                table: "Pack",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Pack_UpdatedById",
                table: "Pack",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PackUsers_PackId",
                table: "PackUsers",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_PackUsers_UserId",
                table: "PackUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PackId",
                table: "Transaction",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackUsers");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Pack");
        }
    }
}
