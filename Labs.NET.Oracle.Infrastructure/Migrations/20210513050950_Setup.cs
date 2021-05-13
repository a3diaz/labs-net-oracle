using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.NET.Oracle.Infrastructure.Migrations
{
    public partial class Setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: true),
                    Lastname = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: true),
                    Gender = table.Column<byte>(type: "NUMBER(3)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    PhoneId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    OwnerId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Type = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.PhoneId);
                    table.UniqueConstraint("AK_Phones_Number", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Phones_Persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_OwnerId",
                table: "Phones",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
