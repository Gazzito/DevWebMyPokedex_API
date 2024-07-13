using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyPokedexAPI.Migrations
{
    /// <inheritdoc />
    public partial class createPokemonInPack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonInPack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PackId = table.Column<int>(type: "integer", nullable: false),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonInPack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonInPack_Pack_PackId",
                        column: x => x.PackId,
                        principalTable: "Pack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInPack_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInPack_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonInPack_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInPack_CreatedById",
                table: "PokemonInPack",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInPack_PackId",
                table: "PokemonInPack",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInPack_PokemonId",
                table: "PokemonInPack",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInPack_UpdatedById",
                table: "PokemonInPack",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonInPack");
        }
    }
}
