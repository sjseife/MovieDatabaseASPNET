namespace Project2.DataContexts.MovieMigrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project2.DataContexts.MovieDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\MovieMigrations";
        }

        protected override void Seed(Project2.DataContexts.MovieDb context)
        {
            context.Movies.AddOrUpdate(
                m=> m.Id,
                new Movie
                {
                    Id = "1",
                    Title = "Terminator 2: Judgement Day",
                    Price = 9.99,
                    Year = 1991,
                    LengthInMinutes = 132,
                    IMDBUrl = "http://www.imdb.com/title/tt0103064/" },
                new Movie
                {
                    Id = "2",
                    Title = "Predator",
                    Price = 2.99,
                    Year = 1987,
                    LengthInMinutes = 107,
                    IMDBUrl = "http://www.imdb.com/title/tt0093773/" },
                new Movie
                {
                    Id = "3",
                    Title = "The Boondock Saints",
                    Price = 10.99,
                    Year = 1999,
                    LengthInMinutes = 108,
                    IMDBUrl = "http://www.imdb.com/title/tt0144117/" },
                new Movie
                {
                    Id = "4",
                    Title = "Fist of Legend",
                    Price = 5.99,
                    Year = 1994,
                    LengthInMinutes = 103,
                    IMDBUrl = "http://www.imdb.com/title/tt0110200/" },
                new Movie
                {
                    Id = "5",
                    Title = "Pirates of the Carribean",
                    Price = 2.99,
                    Year = 2003,
                    LengthInMinutes = 143,
                    IMDBUrl = "http://www.imdb.com/title/tt0325980/" },
                new Movie
                {
                    Id = "6",
                    Title = "Captain America: Civil War",
                    Price = 9.99,
                    Year = 2016,
                    LengthInMinutes = 147,
                    IMDBUrl = "http://www.imdb.com/title/tt3498820/"
                }
            );
        }
    }
}
