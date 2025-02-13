using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTrackingDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeId);
                    table.ForeignKey(
                        name: "FK_Offices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_Assets_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CurrencyName", "ShortName" },
                values: new object[,]
                {
                    { 1, "Svenska Kronor", "SEK" },
                    { 2, "US Dollars", "USD" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "OfficeId", "City", "Country", "CurrencyId" },
                values: new object[,]
                {
                    { 1, "Stockholm", "Sweden", 1 },
                    { 2, "Malmö", "Sweden", 1 },
                    { 3, "New York", "USA", 2 }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssetId", "Brand", "Category", "Configuration", "Model", "OfficeId", "Price", "PurchaseDate" },
                values: new object[,]
                {
                    { 1, "Apple", "Laptop", "8 GB RAM, 256GB SSD", "MacBook Pro", 1, 15000f, "2022-12-10" },
                    { 2, "Samsung", "Phone", "512GB", "S24", 1, 20000f, "2024-11-20" },
                    { 3, "Lenovo", "Laptop", "16GB RAM, 512GB SSD", "ThinkPad", 2, 15499f, "2023-05-17" },
                    { 4, "Apple", "Stationary Computer", "16GB RAM, 1TB SSD", "iMac", 3, 25000f, "2024-02-05" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CurrencyId",
                table: "Offices",
                column: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
