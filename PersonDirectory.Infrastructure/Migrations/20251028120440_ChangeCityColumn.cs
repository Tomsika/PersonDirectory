using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_CityId",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "PersonDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    PersonalNumber = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    PersonDtoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumberDto_PersonDto_PersonDtoId",
                        column: x => x.PersonDtoId,
                        principalTable: "PersonDto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelatedPersonDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RelatedPersonId = table.Column<int>(type: "integer", nullable: false),
                    RelatedPersonFirstName = table.Column<string>(type: "text", nullable: true),
                    RelatedPersonLastName = table.Column<string>(type: "text", nullable: true),
                    RelationType = table.Column<string>(type: "text", nullable: true),
                    PersonDtoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPersonDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedPersonDto_PersonDto_PersonDtoId",
                        column: x => x.PersonDtoId,
                        principalTable: "PersonDto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumberDto_PersonDtoId",
                table: "PhoneNumberDto",
                column: "PersonDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersonDto_PersonDtoId",
                table: "RelatedPersonDto",
                column: "PersonDtoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumberDto");

            migrationBuilder.DropTable(
                name: "RelatedPersonDto");

            migrationBuilder.DropTable(
                name: "PersonDto");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CityId",
                table: "Persons");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                table: "Persons",
                column: "CityId",
                unique: true);
        }
    }
}
