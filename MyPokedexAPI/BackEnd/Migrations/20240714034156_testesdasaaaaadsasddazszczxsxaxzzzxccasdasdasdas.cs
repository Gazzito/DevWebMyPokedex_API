using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPokedexAPI.Migrations
{
    /// <inheritdoc />
    public partial class testesdasaaaaadsasddazszczxsxaxzzzxccasdasdasdas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextOpenExpected",
                table: "PackUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextOpenExpected",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextOpenExpected",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextOpenExpected",
                table: "PackUsers",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
