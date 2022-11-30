using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data
{
    public class ChooseYourMovieDbContext : DbContext
    {
        public ChooseYourMovieDbContext()
        {
            this.Database.EnsureCreated();
        }

        public ChooseYourMovieDbContext(DbContextOptions<ChooseYourMovieDbContext> options) : base(options) { }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Movies.mdf;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Movie movie1 = new Movie() { Id = 1, Name = "movie1", Category = "Action", Duration = 1, Price = 2500 };
            Movie movie2 = new Movie() { Id = 2, Name = "movie2", Category = "Romance", Duration = 2, Price = 10000 };
            Movie movie3 = new Movie() { Id = 3, Name = "movie3", Category = "Comedy", Duration = 1, Price = 20000 };
            Movie movie4 = new Movie() { Id = 4, Name = "movie4", Category = "Horror", Duration = 3, Price = 10000 };
            Movie movie5 = new Movie() { Id = 5, Name = "movie5", Category = "Drama", Duration = 1, Price = 20000 };
            Movie movie6 = new Movie() { Id = 6, Name = "movie6", Category = "Documentary", Duration = 1, Price = 30000 };

            Visitor vis1 = new Visitor() { Id = 1, Name = "Visitor 1 ", PhoneNumber = 0610203050, Address = "Budapest", Email = "vis1@gmail.com" };
            Visitor vis2 = new Visitor() { Id = 2, Name = "Visitor2", PhoneNumber = 0610203750, Address = "Budapest", Email = "vis2@gmail.com" };
            Visitor vis3 = new Visitor() { Id = 3, Name = "Visitor3", PhoneNumber = 0610403050, Address = "Budapest", Email = "vis3@gmail.com" };
            Visitor vis4 = new Visitor() { Id = 4, Name = "Visitor4", PhoneNumber = 0620203050, Address = "Budapest", Email = "vis4@gmail.com" };
            Visitor vis5 = new Visitor() { Id = 5, Name = "Visitor5", PhoneNumber = 0630203050, Address = "Budapest", Email = "vis5@gmail.com" };
            Visitor vis6 = new Visitor() { Id = 6, Name = "Visitor6", PhoneNumber = 0640203050, Address = "Budapest", Email = "vis6@gmail.com" };
            Visitor vis7 = new Visitor() { Id = 7, Name = "Visitor7", PhoneNumber = 0650203050, Address = "Budapest", Email = "vis7@gmail.com" };
            Visitor vis8 = new Visitor() { Id = 8, Name = "Visitor8", PhoneNumber = 0660203050, Address = "Budapest", Email = "vis8@gmail.com" };
            Visitor vis9 = new Visitor() { Id = 9, Name = "Visitor9", PhoneNumber = 0670203050, Address = "Budapest", Email = "vis9@gmail.com" };
            Visitor vis10 = new Visitor() { Id = 10, Name = "Visitor10", PhoneNumber = 0680203050, Address = "Budapest", Email = "vis10@gmail.com" };
            Visitor vis11 = new Visitor() { Id = 11, Name = "Visitor11", PhoneNumber = 0690203050, Address = "Budapest", Email = "vis11@gmail.com" };

            Reservation reservation1 = new Reservation() { Id = 1, VisitorId = vis1.Id, MovieId = movie1.Id, DateTime = new DateTime(2021, 09, 08) };
            Reservation reservation2 = new Reservation() { Id = 2, VisitorId = vis2.Id, MovieId = movie3.Id, DateTime = new DateTime(2021, 09, 09) };
            Reservation reservation3 = new Reservation() { Id = 3, VisitorId = vis5.Id, MovieId = movie2.Id, DateTime = new DateTime(2021, 09, 10) };
            Reservation reservation4 = new Reservation() { Id = 4, VisitorId = vis10.Id, MovieId = movie1.Id, DateTime = new DateTime(2021, 09, 11) };
            Reservation reservation5 = new Reservation() { Id = 5, VisitorId = vis4.Id, MovieId = movie6.Id, DateTime = new DateTime(2021, 09, 12) };
            Reservation reservation6 = new Reservation() { Id = 6, VisitorId = vis11.Id, MovieId = movie2.Id, DateTime = new DateTime(2021, 09, 13) };
            Reservation reservation7 = new Reservation() { Id = 7, VisitorId = vis6.Id, MovieId = movie5.Id, DateTime = new DateTime(2021, 09, 14) };
            Reservation reservation8 = new Reservation() { Id = 8, VisitorId = vis8.Id, MovieId = movie6.Id, DateTime = new DateTime(2021, 09, 15) };
            Reservation reservation9 = new Reservation() { Id = 9, VisitorId = vis3.Id, MovieId = movie4.Id, DateTime = new DateTime(2021, 09, 16) };
            Reservation reservation10 = new Reservation() { Id = 10, VisitorId = vis7.Id, MovieId = movie1.Id, DateTime = new DateTime(2021, 09, 17) };
            Reservation reservation11 = new Reservation() { Id = 11, VisitorId = vis9.Id, MovieId = movie2.Id, DateTime = new DateTime(2021, 09, 18) };

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(reservation => reservation.Movie)
                      .WithMany(movie => movie.Reservation)
                      .HasForeignKey(reservation => reservation.MovieId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(reservation => reservation.Visitor)
                      .WithMany(vis => vis.Reservation)
                      .HasForeignKey(reservation => reservation.VisitorId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Visitor>().HasData(vis1, vis2, vis3, vis4, vis5, vis6, vis7, vis8, vis9, vis10, vis11);
            modelBuilder.Entity<Movie>().HasData(movie1, movie2, movie3, movie4, movie5, movie6);
            modelBuilder.Entity<Reservation>().HasData(reservation1, reservation2, reservation3, reservation4, reservation5, reservation6, reservation7, reservation8, reservation9, reservation10, reservation11);


        }
    }
}
