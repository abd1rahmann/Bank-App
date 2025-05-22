using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "SQL_Latin1_General_CP1_CI_AS");

            // Tabellen 'Accounts' existerar redan, skapas inte igen
            /*
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.AccountId);
                });
            */

            // Andra tabeller som beror på 'Accounts' är också kommenterade
            /*
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    // kolumndefinitioner
                },
                constraints: table =>
                {
                    // nycklar och FK
                });

            migrationBuilder.CreateTable(
                name: "PermenentOrder",
                columns: table => new
                {
                    // kolumndefinitioner
                },
                constraints: table =>
                {
                    // nycklar och FK
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    // kolumndefinitioner
                },
                constraints: table =>
                {
                    // nycklar och FK
                });

            migrationBuilder.CreateTable(
                name: "Dispositions",
                columns: table => new
                {
                    // kolumndefinitioner
                },
                constraints: table =>
                {
                    // nycklar och FK
                });

            migrationBuilder.CreateIndex(name: "IX_Dispositions_AccountId", table: "Dispositions", column: "AccountId");
            migrationBuilder.CreateIndex(name: "IX_Loans_AccountId", table: "Loans", column: "AccountId");
            migrationBuilder.CreateIndex(name: "IX_PermenentOrder_AccountId", table: "PermenentOrder", column: "AccountId");
            migrationBuilder.CreateIndex(name: "IX_Transactions_AccountId", table: "Transactions", column: "AccountId");
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Vi tar inte bort tabeller eftersom de redan finns
        }
    }
}
