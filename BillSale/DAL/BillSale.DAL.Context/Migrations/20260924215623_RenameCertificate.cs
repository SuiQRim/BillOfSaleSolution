using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BillSale.DAL.Context.Migrations
{
    /// <inheritdoc />
    public partial class RenameCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferCertificateProduct");

            migrationBuilder.DropTable(
                name: "TransferCertificates");

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticulNumber = table.Column<int>(type: "integer", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaserId = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PreparationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Companies_PurchaserId",
                        column: x => x.PurchaserId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificates_Companies_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificateProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CertificateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificateProduct_Certificates_CertificateId",
                        column: x => x.CertificateId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificateProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "ArticulNumber", "City", "CreatedAt", "CreatedBy", "DeletedAt", "PreparationDate", "PurchaserId", "SellerId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), 1001, "Санкт-Петербург", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 1, 15, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000002"), 1002, "Москва", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 2, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000003"), 1003, "Псков", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 3, 20, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000004"), 1004, "Великий Новгород", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 4, 8, 13, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000005"), 1005, "Тверь", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 5, 12, 16, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "CertificateProduct",
                columns: new[] { "Id", "CertificateId", "Count", "CreatedAt", "CreatedBy", "DeletedAt", "Price", "ProductId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001"), 5, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 85000m, new Guid("30000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000001"), 5, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 32000m, new Guid("30000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000001"), 5, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 7500m, new Guid("30000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000002"), 10, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 6500m, new Guid("30000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000002"), 20, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 1200m, new Guid("30000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000002"), 4, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 14500m, new Guid("30000000-0000-0000-0000-000000000006"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000002"), 30, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 650m, new Guid("30000000-0000-0000-0000-000000000009"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000003"), 12, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 18500m, new Guid("30000000-0000-0000-0000-000000000007"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000003"), 8, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 22000m, new Guid("30000000-0000-0000-0000-000000000008"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000003"), 6, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 9800m, new Guid("30000000-0000-0000-0000-000000000010"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000011"), new Guid("40000000-0000-0000-0000-000000000004"), 2, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 83000m, new Guid("30000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000012"), new Guid("40000000-0000-0000-0000-000000000004"), 2, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 14000m, new Guid("30000000-0000-0000-0000-000000000006"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000013"), new Guid("40000000-0000-0000-0000-000000000004"), 3, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 6200m, new Guid("30000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000014"), new Guid("40000000-0000-0000-0000-000000000005"), 3, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 31500m, new Guid("30000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000015"), new Guid("40000000-0000-0000-0000-000000000005"), 3, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 7200m, new Guid("30000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000016"), new Guid("40000000-0000-0000-0000-000000000005"), 50, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 620m, new Guid("30000000-0000-0000-0000-000000000009"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificateProduct_CertificateId",
                table: "CertificateProduct",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateProduct_ProductId",
                table: "CertificateProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_PurchaserId",
                table: "Certificates",
                column: "PurchaserId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_SellerId",
                table: "Certificates",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificateProduct");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.CreateTable(
                name: "TransferCertificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticulNumber = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PreparationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferCertificates_Companies_PurchaserId",
                        column: x => x.PurchaserId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferCertificates_Companies_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferCertificateProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TransferCertificateId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCertificateProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferCertificateProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferCertificateProduct_TransferCertificates_TransferCer~",
                        column: x => x.TransferCertificateId,
                        principalTable: "TransferCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransferCertificates",
                columns: new[] { "Id", "ArticulNumber", "City", "CreatedAt", "CreatedBy", "DeletedAt", "PreparationDate", "PurchaserId", "SellerId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), 1001, "Санкт-Петербург", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 1, 15, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000002"), 1002, "Москва", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 2, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000003"), 1003, "Псков", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 3, 20, 10, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000004"), 1004, "Великий Новгород", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 4, 8, 13, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("40000000-0000-0000-0000-000000000005"), 1005, "Тверь", new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, new DateTimeOffset(new DateTime(2026, 5, 12, 16, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "TransferCertificateProduct",
                columns: new[] { "Id", "Count", "CreatedAt", "CreatedBy", "DeletedAt", "Price", "ProductId", "TransferCertificateId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 5, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 85000m, new Guid("30000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 5, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 32000m, new Guid("30000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), 5, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 7500m, new Guid("30000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), 10, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 6500m, new Guid("30000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 20, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 1200m, new Guid("30000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 4, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 14500m, new Guid("30000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000007"), 30, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 650m, new Guid("30000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000008"), 12, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 18500m, new Guid("30000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000009"), 8, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 22000m, new Guid("30000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000010"), 6, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 9800m, new Guid("30000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000003"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000011"), 2, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 83000m, new Guid("30000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000012"), 2, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 14000m, new Guid("30000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000013"), 3, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 6200m, new Guid("30000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000004"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000014"), 3, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 31500m, new Guid("30000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000015"), 3, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 7200m, new Guid("30000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" },
                    { new Guid("50000000-0000-0000-0000-000000000016"), 50, new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, 620m, new Guid("30000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000005"), new DateTimeOffset(new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferCertificateProduct_ProductId",
                table: "TransferCertificateProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCertificateProduct_TransferCertificateId",
                table: "TransferCertificateProduct",
                column: "TransferCertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCertificates_PurchaserId",
                table: "TransferCertificates",
                column: "PurchaserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCertificates_SellerId",
                table: "TransferCertificates",
                column: "SellerId");
        }
    }
}
