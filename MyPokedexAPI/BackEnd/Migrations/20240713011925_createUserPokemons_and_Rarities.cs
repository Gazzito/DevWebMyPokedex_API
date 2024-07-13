using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyPokedexAPI.Migrations
{
    /// <inheritdoc />
    public partial class createUserPokemons_and_Rarities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    ActualAttackPoints = table.Column<int>(type: "integer", nullable: false),
                    ActualHealthPoints = table.Column<int>(type: "integer", nullable: false),
                    ActualDefensePoints = table.Column<int>(type: "integer", nullable: false),
                    ActualSpeedPoints = table.Column<int>(type: "integer", nullable: false),
                    TotalCombatPoints = table.Column<int>(type: "integer", nullable: false),
                    Rarity = table.Column<string>(type: "text", nullable: false),
                    UserPackId = table.Column<int>(type: "integer", nullable: false),
                    IsFavourite = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPokemons_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPokemons_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPokemons_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPokemons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPokemons_CreatedById",
                table: "UserPokemons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPokemons_PokemonId",
                table: "UserPokemons",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPokemons_UpdatedById",
                table: "UserPokemons",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPokemons_UserId",
                table: "UserPokemons",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPokemons");
        }
    }
}
