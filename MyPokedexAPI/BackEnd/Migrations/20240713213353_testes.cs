using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPokedexAPI.Migrations
{
    /// <inheritdoc />
    public partial class testes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPokemons_PackUsers_UserPackId",
                table: "UserPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPokemons_Pokemons_PokemonId",
                table: "UserPokemons");

            migrationBuilder.RenameColumn(
                name: "UserPackId",
                table: "UserPokemons",
                newName: "PackId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPokemons_UserPackId",
                table: "UserPokemons",
                newName: "IX_UserPokemons_PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPokemons_Packs_PackId",
                table: "UserPokemons",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPokemons_Pokemons_PokemonId",
                table: "UserPokemons",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPokemons_Packs_PackId",
                table: "UserPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPokemons_Pokemons_PokemonId",
                table: "UserPokemons");

            migrationBuilder.RenameColumn(
                name: "PackId",
                table: "UserPokemons",
                newName: "UserPackId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPokemons_PackId",
                table: "UserPokemons",
                newName: "IX_UserPokemons_UserPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPokemons_PackUsers_UserPackId",
                table: "UserPokemons",
                column: "UserPackId",
                principalTable: "PackUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPokemons_Pokemons_PokemonId",
                table: "UserPokemons",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
