using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRoles.Migrations
{
    /// <inheritdoc />
    public partial class seperateteammembersandaboutus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_AboutUs_AboutUsSectionId",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_AboutUsSectionId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "AboutUsSectionId",
                table: "TeamMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AboutUsSectionId",
                table: "TeamMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_AboutUsSectionId",
                table: "TeamMembers",
                column: "AboutUsSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_AboutUs_AboutUsSectionId",
                table: "TeamMembers",
                column: "AboutUsSectionId",
                principalTable: "AboutUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
