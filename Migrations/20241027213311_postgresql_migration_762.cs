using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gestion_Fimas_Digitales.Migrations
{
    /// <inheritdoc />
    public partial class postgresql_migration_762 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "firmaDigital",
                columns: table => new
                {
                    id_firma = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_firma = table.Column<char>(type: "character(1)", nullable: false),
                    razon_social = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    representante_legal = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    empresa_acreditadora = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    fecha_emision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ruta_rubrica = table.Column<string>(type: "text", nullable: false),
                    certificado_digital = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_firmaDigital", x => x.id_firma);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "firmaDigital");
        }
    }
}
