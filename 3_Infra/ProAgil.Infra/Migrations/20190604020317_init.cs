using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Infra.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "lower(hex(randomblob(16)))"),
                    Local = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DataEvento = table.Column<DateTime>(nullable: false),
                    Tema = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    QtdPessoas = table.Column<int>(unicode: false, nullable: false),
                    ImgUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palestrante",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "lower(hex(randomblob(16)))"),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    MiniCurriculo = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ImgUrl = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    EventoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestrante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "lower(hex(randomblob(16)))"),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Preco = table.Column<decimal>(unicode: false, nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: true),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Quantidade = table.Column<int>(unicode: false, nullable: false),
                    EventoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lote_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PalestranteEvento",
                columns: table => new
                {
                    EventoId = table.Column<Guid>(nullable: false),
                    PalestranteId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "lower(hex(randomblob(16)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalestranteEvento", x => new { x.EventoId, x.PalestranteId });
                    table.ForeignKey(
                        name: "FK_PalestranteEvento_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PalestranteEvento_Palestrante_PalestranteId",
                        column: x => x.PalestranteId,
                        principalTable: "Palestrante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedeSocial",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "lower(hex(randomblob(16)))"),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    EventoId = table.Column<Guid>(nullable: true),
                    PalestranteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedeSocial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedeSocial_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RedeSocial_Palestrante_PalestranteId",
                        column: x => x.PalestranteId,
                        principalTable: "Palestrante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lote_EventoId",
                table: "Lote",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_PalestranteEvento_PalestranteId",
                table: "PalestranteEvento",
                column: "PalestranteId");

            migrationBuilder.CreateIndex(
                name: "IX_RedeSocial_EventoId",
                table: "RedeSocial",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_RedeSocial_PalestranteId",
                table: "RedeSocial",
                column: "PalestranteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lote");

            migrationBuilder.DropTable(
                name: "PalestranteEvento");

            migrationBuilder.DropTable(
                name: "RedeSocial");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Palestrante");
        }
    }
}
