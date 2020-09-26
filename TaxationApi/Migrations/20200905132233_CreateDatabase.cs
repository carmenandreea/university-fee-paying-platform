using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaxationApi.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    institutionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.institutionId);
                });

            migrationBuilder.CreateTable(
                name: "Payers",
                columns: table => new
                {
                    payerId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    cnp = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payers", x => x.payerId);
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    hasReadingPermission = table.Column<bool>(nullable: false),
                    hasWritingPermission = table.Column<bool>(nullable: false),
                    institutionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.userId);
                    table.ForeignKey(
                        name: "FK_AdminUsers_Institutions_institutionId",
                        column: x => x.institutionId,
                        principalTable: "Institutions",
                        principalColumn: "institutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxValues",
                columns: table => new
                {
                    taxValueId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    institutionId = table.Column<long>(nullable: false),
                    value = table.Column<long>(nullable: false),
                    type = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxValues", x => x.taxValueId);
                    table.ForeignKey(
                        name: "FK_TaxValues_Institutions_institutionId",
                        column: x => x.institutionId,
                        principalTable: "Institutions",
                        principalColumn: "institutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transactionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    institutionId = table.Column<long>(nullable: false),
                    payerId = table.Column<long>(nullable: false),
                    status = table.Column<long>(nullable: false),
                    value = table.Column<long>(nullable: false),
                    type = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.transactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Institutions_institutionId",
                        column: x => x.institutionId,
                        principalTable: "Institutions",
                        principalColumn: "institutionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Payers_payerId",
                        column: x => x.payerId,
                        principalTable: "Payers",
                        principalColumn: "payerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_institutionId",
                table: "AdminUsers",
                column: "institutionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxValues_institutionId",
                table: "TaxValues",
                column: "institutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_institutionId",
                table: "Transactions",
                column: "institutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_payerId",
                table: "Transactions",
                column: "payerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "TaxValues");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "Payers");
        }
    }
}
