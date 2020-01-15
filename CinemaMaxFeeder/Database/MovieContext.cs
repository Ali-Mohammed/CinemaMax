using CinemaMaxFeeder.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaMaxFeeder
{
    public class MovieContext : DbContext
    {
        public DbSet<StorageServer> StorageServers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<HomePageSlider> HomePageSliders { get; set; }
        public DbSet<HomePageSliderMovie> HomePageSliderMovie { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.SQLBaseStringConnection());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StorageServer>()
            .HasMany(c => c.Movies)
            .WithOne(e => e.StorageServer);

            modelBuilder.Entity<Movie>()
                .HasMany(c => c.SkippingDurationsStart)
                .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.Translations)
            .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.IntroSkipping)
            .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.Categories)
            .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.DirectorsInfo)
            .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.ActorsInfo)
            .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.WritersInfo)
            .WithOne(e => e.Movie);

            modelBuilder.Entity<Movie>()
            .HasMany(c => c.Comments)
            .WithOne(e => e.Movie);


            modelBuilder.Entity<Movie>()
            .HasMany(c => c.TranscoddedFiles)
            .WithOne(e => e.Movie);


            modelBuilder.Entity<HomePageSliderMovie>()
                .HasKey(t => new { t.HomePageSliderId, t.MovieId });

            modelBuilder.Entity<HomePageSliderMovie>()
                .HasOne(pt => pt.HomePageSlider)
                .WithMany(p => p.HomePageSliderMovies)
                .HasForeignKey(pt => pt.HomePageSliderId);


            modelBuilder.Entity<HomePageSliderMovie>()
                .HasOne(pt => pt.Movie)
                .WithMany(t => t.HomePageSliderMovies)
                .HasForeignKey(pt => pt.MovieId);
        }
    }


}
