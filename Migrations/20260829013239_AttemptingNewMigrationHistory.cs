using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumMovieTheater.Migrations
{
    /// <inheritdoc />
    public partial class AttemptingNewMigrationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "concessionitems",
                columns: table => new
                {
                    ConcessionItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Category = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LowStockThreshold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_concessionitems", x => x.ConcessionItemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReleaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Genre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RuntimeMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.MovieId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "theater",
                columns: table => new
                {
                    TheaterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theater", x => x.TheaterId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "auditorium",
                columns: table => new
                {
                    AuditoriumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TheaterId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditorium", x => x.AuditoriumId);
                    table.ForeignKey(
                        name: "FK_auditorium_theater_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "theater",
                        principalColumn: "TheaterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "concessioninventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConcessionItemId = table.Column<int>(type: "int", nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_concessioninventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_concessioninventory_concessionitems_ConcessionItemId",
                        column: x => x.ConcessionItemId,
                        principalTable: "concessionitems",
                        principalColumn: "ConcessionItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_concessioninventory_theater_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "theater",
                        principalColumn: "TheaterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "showtimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false),
                    AuditoriumId = table.Column<int>(type: "int", nullable: false),
                    ShowDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimeSlot = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_showtimes_auditorium_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalTable: "auditorium",
                        principalColumn: "AuditoriumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_showtimes_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_showtimes_theater_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "theater",
                        principalColumn: "TheaterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventorytransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    QuantityChange = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventorytransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_inventorytransactions_concessioninventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "concessioninventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShowtimeId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Kids = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BookedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ConfirmationCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookings_showtimes_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalTable: "showtimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_orders_bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ConcessionItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderitems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_orderitems_concessionitems_ConcessionItemId",
                        column: x => x.ConcessionItemId,
                        principalTable: "concessionitems",
                        principalColumn: "ConcessionItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderitems_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_auditorium_TheaterId",
                table: "auditorium",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_ShowtimeId",
                table: "bookings",
                column: "ShowtimeId");

            migrationBuilder.CreateIndex(
                name: "IX_concessioninventory_ConcessionItemId",
                table: "concessioninventory",
                column: "ConcessionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_concessioninventory_TheaterId",
                table: "concessioninventory",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_inventorytransactions_InventoryId",
                table: "inventorytransactions",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_ConcessionItemId",
                table: "orderitems",
                column: "ConcessionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_OrderId",
                table: "orderitems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_BookingId",
                table: "orders",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_showtimes_AuditoriumId",
                table: "showtimes",
                column: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_showtimes_MovieId",
                table: "showtimes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_showtimes_TheaterId",
                table: "showtimes",
                column: "TheaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventorytransactions");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "concessioninventory");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "concessionitems");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "showtimes");

            migrationBuilder.DropTable(
                name: "auditorium");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "theater");
        }
    }
}
