﻿// <auto-generated />
using System;
using CinemaMaxFeeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaMaxFeeder.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20200108103938_CinemaMaxV1")]
    partial class CinemaMaxV1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.ActorsInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Nb")
                        .HasColumnType("bigint");

                    b.Property<long>("Role")
                        .HasColumnType("bigint");

                    b.Property<string>("StaffImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffImgMediumThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffImgThumb")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorsInfo");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.DirectorsInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Nb")
                        .HasColumnType("bigint");

                    b.Property<long>("Role")
                        .HasColumnType("bigint");

                    b.Property<string>("StaffImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffImgMediumThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffImgThumb")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("DirectorsInfo");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArTranslationFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArTranslationFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("AudioStreamNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("CacheShort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Castable")
                        .HasColumnType("bit");

                    b.Property<long>("CollectionId")
                        .HasColumnType("bigint");

                    b.Property<string>("DownloadId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DownloadRetry")
                        .HasColumnType("bigint");

                    b.Property<int>("DownloadStatus")
                        .HasColumnType("int");

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnTranslationFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnTranslationFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EpisodeFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EpisodeNummer")
                        .HasColumnType("bigint");

                    b.Property<string>("FileFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FilmRating")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FinishDownloadAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasIntroSkipping")
                        .HasColumnType("bit");

                    b.Property<string>("ImdbUrlRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBanner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgMediumThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgMediumThumbObjUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgObjUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgThumbObjUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IsDeleted")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSlideShow")
                        .HasColumnType("bit");

                    b.Property<long>("IsSpecial")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("ItemDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("Kind")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("MDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Nb")
                        .HasColumnType("bigint");

                    b.Property<string>("OtherTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ParentSkipping")
                        .HasColumnType("bigint");

                    b.Property<long>("Priority")
                        .HasColumnType("bigint");

                    b.Property<long>("Rate")
                        .HasColumnType("bigint");

                    b.Property<long>("RootSeries")
                        .HasColumnType("bigint");

                    b.Property<long>("Season")
                        .HasColumnType("bigint");

                    b.Property<long>("SeriesRating")
                        .HasColumnType("bigint");

                    b.Property<string>("ShowComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpTranslationFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stars")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDownloadAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Trailer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UseParentImg")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoCommentsNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoDisLikesNumber")
                        .HasColumnType("bigint");

                    b.Property<long?>("VideoLanguagesId")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoLikesNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoViewsNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("VideoLanguagesId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.SkippingDurations", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("SkippingDurations");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.WritersInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Nb")
                        .HasColumnType("bigint");

                    b.Property<long>("Role")
                        .HasColumnType("bigint");

                    b.Property<string>("StaffImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffImgMediumThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffImgThumb")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("WritersInfo");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.CommentsJson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("CommentNb")
                        .HasColumnType("bigint");

                    b.Property<string>("ImgObjUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgThumbObjUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MitgliedImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MitgliedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MitgliedNb")
                        .HasColumnType("bigint");

                    b.Property<string>("MitgliedThumbImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<long>("Nb")
                        .HasColumnType("bigint");

                    b.Property<string>("ObjectUrlExpiration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Permission")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdateDateTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.IntroSkippingJson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ControlLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("IntroSkipping");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.TranscoddedFilesJson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Container")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DownloadId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DownloadLocalPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DownloadRetry")
                        .HasColumnType("bigint");

                    b.Property<int>("DownloadStatus")
                        .HasColumnType("int");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FinishDownloadAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ServerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDownloadAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("TranscoddedFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("TranscoddedFiles");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.TranslationJson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DownloadCompleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DownloadId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DownloadRetry")
                        .HasColumnType("int");

                    b.Property<DateTime>("DownloadStarted")
                        .HasColumnType("datetime2");

                    b.Property<int>("DownloadStatus")
                        .HasColumnType("int");

                    b.Property<string>("Extention")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<long>("NB")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Translation");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.VideoLanguagesJson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("VideoLanguages");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.ActorsInfo", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("ActorsInfo")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.DirectorsInfo", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("DirectorsInfo")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.Movie", b =>
                {
                    b.HasOne("CinemaMaxFeeder.ModelJson.VideoLanguagesJson", "VideoLanguages")
                        .WithMany()
                        .HasForeignKey("VideoLanguagesId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.SkippingDurations", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("SkippingDurationsStart")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.Database.Model.WritersInfo", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("WritersInfo")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.CommentsJson", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("Comments")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.IntroSkippingJson", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("IntroSkipping")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.TranscoddedFilesJson", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("TranscoddedFiles")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.TranslationJson", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("Translations")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("CinemaMaxFeeder.ModelJson.VideoLanguagesJson", b =>
                {
                    b.HasOne("CinemaMaxFeeder.Database.Model.Movie", null)
                        .WithMany("Categories")
                        .HasForeignKey("MovieId");
                });
#pragma warning restore 612, 618
        }
    }
}