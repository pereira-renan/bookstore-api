using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstore_api.Migrations.Books
{
    public partial class Books : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelfLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "AccessInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Viewability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Embeddable = table.Column<bool>(type: "bit", nullable: true),
                    PublicDomain = table.Column<bool>(type: "bit", nullable: true),
                    TextToSpeechPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebReaderLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessViewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuoteSharingAllowed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessInfo_Books_Id",
                        column: x => x.Id,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saleability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEbook = table.Column<bool>(type: "bit", nullable: true),
                    BuyLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleInfo_Books_Id",
                        column: x => x.Id,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextSnippet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchInfo_Books_Id",
                        column: x => x.Id,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolumeInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Authors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageRating = table.Column<double>(type: "float", nullable: true),
                    RatingsCount = table.Column<int>(type: "int", nullable: true),
                    MaturityRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowAnonLogging = table.Column<bool>(type: "bit", nullable: true),
                    ContentVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviewLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanonicalVolumeLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeInfo_Books_Id",
                        column: x => x.Id,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Epub",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    AcsTokenLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epub_AccessInfo_Id",
                        column: x => x.Id,
                        principalTable: "AccessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pdf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    AcsTokenLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pdf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pdf_AccessInfo_Id",
                        column: x => x.Id,
                        principalTable: "AccessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountInMicros = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListPrice_SaleInfo_Id",
                        column: x => x.Id,
                        principalTable: "SaleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountInMicros = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPrice_SaleInfo_Id",
                        column: x => x.Id,
                        principalTable: "SaleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SmallThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageLinks_VolumeInfo_Id",
                        column: x => x.Id,
                        principalTable: "VolumeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PanelizationSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainsEpubBubbles = table.Column<bool>(type: "bit", nullable: true),
                    ContainsImageBubbles = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanelizationSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanelizationSummary_VolumeInfo_Id",
                        column: x => x.Id,
                        principalTable: "VolumeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingModes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<bool>(type: "bit", nullable: true),
                    Image = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingModes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingModes_VolumeInfo_Id",
                        column: x => x.Id,
                        principalTable: "VolumeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Epub");

            migrationBuilder.DropTable(
                name: "ImageLinks");

            migrationBuilder.DropTable(
                name: "ListPrice");

            migrationBuilder.DropTable(
                name: "PanelizationSummary");

            migrationBuilder.DropTable(
                name: "Pdf");

            migrationBuilder.DropTable(
                name: "ReadingModes");

            migrationBuilder.DropTable(
                name: "RetailPrice");

            migrationBuilder.DropTable(
                name: "SearchInfo");

            migrationBuilder.DropTable(
                name: "AccessInfo");

            migrationBuilder.DropTable(
                name: "VolumeInfo");

            migrationBuilder.DropTable(
                name: "SaleInfo");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
