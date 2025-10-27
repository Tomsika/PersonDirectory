using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
