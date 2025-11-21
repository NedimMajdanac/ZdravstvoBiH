using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Dijagnoze : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dijagnoze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICD10Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumDijagnoze = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacijentId = table.Column<int>(type: "int", nullable: false),
                    DoktorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dijagnoze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dijagnoze_Doktori_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dijagnoze_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dijagnoze_DoktorId",
                table: "Dijagnoze",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Dijagnoze_PacijentId",
                table: "Dijagnoze",
                column: "PacijentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dijagnoze");
        }
    }
}
