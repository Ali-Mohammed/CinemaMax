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
        public DbSet<Movie> Movies { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.SqlServerConnectionString);
        }
    }

    public class MovieDto
    {
        [Key]
        [Column(name: "Id")]
        public int Id { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
    }

}
