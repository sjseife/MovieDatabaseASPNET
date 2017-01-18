using Project2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project2.DataContexts
{
    public class MovieDb : DbContext
    {
        public MovieDb() : base("name=DefaultConnection")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<ViewedMovie> ViewedMovies { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}