using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zdravstvo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MedicinskiKartonmodeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicinskiKartoni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alergije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vakcinacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KrvnaGrupa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HronicneBolesti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operacije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PorodicnaAnamneza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Terapije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacijentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinskiKartoni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicinskiKartoni_Pacijenti_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicinskiKartoni_PacijentId",
                table: "MedicinskiKartoni",
                column: "PacijentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicinskiKartoni");
        }
    }
}
