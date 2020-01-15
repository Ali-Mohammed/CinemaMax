using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaMaxFeeder.Migrations
{
    public partial class CinemaV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePageSliders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<long>(nullable: false),
                    Nb = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Options = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageSliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageServers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IP = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    ServerLoad = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePageSliderMovie",
                columns: table => new
                {
                    MovieId = table.Column<long>(nullable: false),
                    HomePageSliderId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageSliderMovie", x => new { x.HomePageSliderId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_HomePageSliderMovie_HomePageSliders_HomePageSliderId",
                        column: x => x.HomePageSliderId,
                        principalTable: "HomePageSliders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorsInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nb = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<long>(nullable: false),
                    StaffImg = table.Column<string>(nullable: true),
                    StaffImgThumb = table.Column<string>(nullable: true),
                    StaffImgMediumThumb = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorsInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nb = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    MitgliedNb = table.Column<long>(nullable: false),
                    MitgliedName = table.Column<string>(nullable: true),
                    MitgliedImg = table.Column<string>(nullable: true),
                    MitgliedThumbImg = table.Column<string>(nullable: true),
                    ImgObjUrl = table.Column<string>(nullable: true),
                    ImgThumbObjUrl = table.Column<string>(nullable: true),
                    ObjectUrlExpiration = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<long>(nullable: false),
                    CommentNb = table.Column<long>(nullable: false),
                    Permission = table.Column<long>(nullable: false),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectorsInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nb = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<long>(nullable: false),
                    StaffImg = table.Column<string>(nullable: true),
                    StaffImgThumb = table.Column<string>(nullable: true),
                    StaffImgMediumThumb = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorsInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntroSkipping",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<string>(nullable: true),
                    End = table.Column<string>(nullable: true),
                    ControlLevel = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntroSkipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkippingDurations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<string>(nullable: true),
                    End = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkippingDurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranscoddedFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Resolution = table.Column<string>(nullable: true),
                    Container = table.Column<string>(nullable: true),
                    TranscoddedFileName = table.Column<string>(nullable: true),
                    VideoUrl = table.Column<string>(nullable: true),
                    DownloadStatus = table.Column<int>(nullable: false),
                    DownloadId = table.Column<string>(nullable: true),
                    StartDownloadAt = table.Column<DateTime>(nullable: false),
                    FinishDownloadAt = table.Column<DateTime>(nullable: false),
                    DownloadRetry = table.Column<long>(nullable: false),
                    DownloadLocalPath = table.Column<string>(nullable: true),
                    ServerId = table.Column<long>(nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscoddedFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NB = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Extention = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    DownloadStatus = table.Column<int>(nullable: false),
                    DownloadStarted = table.Column<DateTime>(nullable: false),
                    DownloadCompleted = table.Column<DateTime>(nullable: false),
                    DownloadRetry = table.Column<int>(nullable: false),
                    DownloadId = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nb = table.Column<long>(nullable: false),
                    Priority = table.Column<long>(nullable: false),
                    IsSlideShow = table.Column<bool>(nullable: false),
                    EnTitle = table.Column<string>(nullable: true),
                    ArTitle = table.Column<string>(nullable: true),
                    OtherTitle = table.Column<string>(nullable: true),
                    Stars = table.Column<decimal>(nullable: false),
                    EnTranslationFile = table.Column<string>(nullable: true),
                    ArTranslationFile = table.Column<string>(nullable: true),
                    FileFile = table.Column<string>(nullable: true),
                    ArContent = table.Column<string>(nullable: true),
                    EnContent = table.Column<string>(nullable: true),
                    MDate = table.Column<DateTime>(nullable: false),
                    Year = table.Column<long>(nullable: false),
                    Kind = table.Column<long>(nullable: false),
                    Season = table.Column<long>(nullable: false),
                    Img = table.Column<string>(nullable: true),
                    ImgThumb = table.Column<string>(nullable: true),
                    ImgMediumThumb = table.Column<string>(nullable: true),
                    ImgObjUrl = table.Column<string>(nullable: true),
                    ImgMediumThumbObjUrl = table.Column<string>(nullable: true),
                    FilmRating = table.Column<long>(nullable: false),
                    SeriesRating = table.Column<long>(nullable: false),
                    EpisodeNummer = table.Column<long>(nullable: false),
                    Rate = table.Column<long>(nullable: false),
                    IsSpecial = table.Column<long>(nullable: false),
                    ItemDate = table.Column<DateTimeOffset>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    ImdbUrlRef = table.Column<string>(nullable: true),
                    ImgBanner = table.Column<string>(nullable: true),
                    RootSeries = table.Column<long>(nullable: false),
                    UseParentImg = table.Column<long>(nullable: false),
                    SpTranslationFile = table.Column<string>(nullable: true),
                    ShowComments = table.Column<string>(nullable: true),
                    EpisodeFlag = table.Column<string>(nullable: true),
                    Trailer = table.Column<string>(nullable: true),
                    AudioStreamNumber = table.Column<long>(nullable: false),
                    ParentSkipping = table.Column<long>(nullable: false),
                    CollectionId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<long>(nullable: false),
                    CacheShort = table.Column<string>(nullable: true),
                    ImgThumbObjUrl = table.Column<string>(nullable: true),
                    ArTranslationFilePath = table.Column<string>(nullable: true),
                    EnTranslationFilePath = table.Column<string>(nullable: true),
                    HasIntroSkipping = table.Column<bool>(nullable: false),
                    VideoLikesNumber = table.Column<long>(nullable: false),
                    VideoDisLikesNumber = table.Column<long>(nullable: false),
                    VideoCommentsNumber = table.Column<long>(nullable: false),
                    VideoViewsNumber = table.Column<long>(nullable: false),
                    Castable = table.Column<bool>(nullable: false),
                    StartDownloadAt = table.Column<DateTime>(nullable: false),
                    FinishDownloadAt = table.Column<DateTime>(nullable: false),
                    DownloadRetry = table.Column<long>(nullable: false),
                    DownloadId = table.Column<string>(nullable: true),
                    VideoLanguagesId = table.Column<long>(nullable: true),
                    DownloadStatus = table.Column<int>(nullable: false),
                    StorageServerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_StorageServers_StorageServerId",
                        column: x => x.StorageServerId,
                        principalTable: "StorageServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_VideoLanguages_VideoLanguagesId",
                        column: x => x.VideoLanguagesId,
                        principalTable: "VideoLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WritersInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nb = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<long>(nullable: false),
                    StaffImg = table.Column<string>(nullable: true),
                    StaffImgThumb = table.Column<string>(nullable: true),
                    StaffImgMediumThumb = table.Column<string>(nullable: true),
                    MovieId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritersInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WritersInfo_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorsInfo_MovieId",
                table: "ActorsInfo",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorsInfo_MovieId",
                table: "DirectorsInfo",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageSliderMovie_MovieId",
                table: "HomePageSliderMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_IntroSkipping_MovieId",
                table: "IntroSkipping",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_StorageServerId",
                table: "Movies",
                column: "StorageServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_VideoLanguagesId",
                table: "Movies",
                column: "VideoLanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_SkippingDurations_MovieId",
                table: "SkippingDurations",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscoddedFiles_MovieId",
                table: "TranscoddedFiles",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_MovieId",
                table: "Translation",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoLanguages_MovieId",
                table: "VideoLanguages",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WritersInfo_MovieId",
                table: "WritersInfo",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePageSliderMovie_Movies_MovieId",
                table: "HomePageSliderMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorsInfo_Movies_MovieId",
                table: "ActorsInfo",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Movies_MovieId",
                table: "Comments",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorsInfo_Movies_MovieId",
                table: "DirectorsInfo",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntroSkipping_Movies_MovieId",
                table: "IntroSkipping",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkippingDurations_Movies_MovieId",
                table: "SkippingDurations",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TranscoddedFiles_Movies_MovieId",
                table: "TranscoddedFiles",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Movies_MovieId",
                table: "Translation",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoLanguages_Movies_MovieId",
                table: "VideoLanguages",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoLanguages_Movies_MovieId",
                table: "VideoLanguages");

            migrationBuilder.DropTable(
                name: "ActorsInfo");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DirectorsInfo");

            migrationBuilder.DropTable(
                name: "HomePageSliderMovie");

            migrationBuilder.DropTable(
                name: "IntroSkipping");

            migrationBuilder.DropTable(
                name: "SkippingDurations");

            migrationBuilder.DropTable(
                name: "TranscoddedFiles");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "WritersInfo");

            migrationBuilder.DropTable(
                name: "HomePageSliders");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "StorageServers");

            migrationBuilder.DropTable(
                name: "VideoLanguages");
        }
    }
}
