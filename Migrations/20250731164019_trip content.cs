using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRoles.Migrations
{
    /// <inheritdoc />
    public partial class tripcontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrekPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxAltitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekkingDistance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestSeason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrekCostInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekCostInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekCostInfos_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekFAQs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekFAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekFAQs_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekGalleryImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekGalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekGalleryImages_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekHighlights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekHighlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekHighlights_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekInclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false),
                    TrekPackageId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekInclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekInclusions_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrekInclusions_TrekPackages_TrekPackageId1",
                        column: x => x.TrekPackageId1,
                        principalTable: "TrekPackages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrekItineraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekItineraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekItineraries_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekOverviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyPoints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekOverviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekOverviews_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrekPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekReviews_TrekPackages_TrekPackageId",
                        column: x => x.TrekPackageId,
                        principalTable: "TrekPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrekPriceBreakdowns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Included = table.Column<bool>(type: "bit", nullable: false),
                    TrekCostInfoId = table.Column<int>(type: "int", nullable: false),
                    TrekCostInfoId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekPriceBreakdowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekPriceBreakdowns_TrekCostInfos_TrekCostInfoId1",
                        column: x => x.TrekCostInfoId1,
                        principalTable: "TrekCostInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrekItineraryDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Altitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accommodation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrekItineraryId = table.Column<int>(type: "int", nullable: false),
                    TrekItineraryId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrekItineraryDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrekItineraryDays_TrekItineraries_TrekItineraryId1",
                        column: x => x.TrekItineraryId1,
                        principalTable: "TrekItineraries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrekCostInfos_TrekPackageId",
                table: "TrekCostInfos",
                column: "TrekPackageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrekFAQs_TrekPackageId",
                table: "TrekFAQs",
                column: "TrekPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrekGalleryImages_TrekPackageId",
                table: "TrekGalleryImages",
                column: "TrekPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrekHighlights_TrekPackageId",
                table: "TrekHighlights",
                column: "TrekPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrekInclusions_TrekPackageId",
                table: "TrekInclusions",
                column: "TrekPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrekInclusions_TrekPackageId1",
                table: "TrekInclusions",
                column: "TrekPackageId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrekItineraries_TrekPackageId",
                table: "TrekItineraries",
                column: "TrekPackageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrekItineraryDays_TrekItineraryId1",
                table: "TrekItineraryDays",
                column: "TrekItineraryId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrekOverviews_TrekPackageId",
                table: "TrekOverviews",
                column: "TrekPackageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrekPriceBreakdowns_TrekCostInfoId1",
                table: "TrekPriceBreakdowns",
                column: "TrekCostInfoId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrekReviews_TrekPackageId",
                table: "TrekReviews",
                column: "TrekPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrekFAQs");

            migrationBuilder.DropTable(
                name: "TrekGalleryImages");

            migrationBuilder.DropTable(
                name: "TrekHighlights");

            migrationBuilder.DropTable(
                name: "TrekInclusions");

            migrationBuilder.DropTable(
                name: "TrekItineraryDays");

            migrationBuilder.DropTable(
                name: "TrekOverviews");

            migrationBuilder.DropTable(
                name: "TrekPriceBreakdowns");

            migrationBuilder.DropTable(
                name: "TrekReviews");

            migrationBuilder.DropTable(
                name: "TrekItineraries");

            migrationBuilder.DropTable(
                name: "TrekCostInfos");

            migrationBuilder.DropTable(
                name: "TrekPackages");
        }
    }
}
