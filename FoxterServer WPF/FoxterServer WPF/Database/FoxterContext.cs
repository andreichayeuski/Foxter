using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ClassLibrary;

namespace FoxterServer_WPF
{
    public class FoxterContext : DbContext
    {
        public FoxterContext() :
            base("FoxterDB")
        {

        }

        public void CheckAndUpdateFilmInTable(List<Film> films)
        {
            foreach (Film a in films)
            {
                if (this.Films.Count<Film>() == 0 || !this.Films.Any(u => (u.Name == a.Name)))
                {
                    this.Films.Add(a);
                }
                else
                {
                    Console.WriteLine("This element is in table");
                }
                Console.WriteLine(a.Name);
            }
            this.SaveChanges();
        }

        public void CheckAndUpdateCinemaInTable(List<Cinema> cinemas)
        {
            foreach (Cinema a in cinemas)
            {
                if (this.Cinemas.Count<Cinema>() == 0 || !this.Cinemas.Any(u => (u.Name == a.Name)))
                {
                    this.Cinemas.Add(a);
                }
                else
                {
                    Console.WriteLine("This element is in table");
                }
                Console.WriteLine(a.Name);
            }
            this.SaveChanges();
        }

        public void MakeListOfSessionAndAddToDatabase(List<SessionFromFile> source)
        {
            List<Cinema> cinema_list = new List<Cinema>();
            cinema_list.AddRange(this.Cinemas);
            foreach (SessionFromFile s in source)
            {
                for (int i = 0; i < cinema_list.Count; i++)
                {
                    if (cinema_list[i].Link.Equals(s.CinemaLink))
                    {
                        foreach (Film f in this.Films)
                        {
                            if (f.Link.Equals(s.FilmLink))
                            {
                                Session session = new Session()
                                {
                                    Cinema = cinema_list[i],
                                    Film = f,
                                    Date = s.Date,
                                    Time = s.Time
                                };
                                
                                this.Cinemas.Find(cinema_list[i].Id).Sessions.Add(session);
                                f.Sessions.Add(session);
                                this.Sessions.Add(session);
                            }
                        }
                    }
                }
            }
            this.SaveChanges();
        }

        public void CheckAndUpdateConcertInTable(List<Concert> concerts)
        {
            foreach (Concert a in concerts)
            {
                if (this.Concerts.Count<Concert>() == 0 || !this.Concerts.Any(u => (u.Name == a.Name)))
                {
                    this.Concerts.Add(a);
                }
                else
                {
                    Console.WriteLine("This element is in table");
                }
                Console.WriteLine(a.Name);
            }
            this.SaveChanges();
        }

        public void CheckAndUpdateExhibitionInTable(List<Exhibition> exhibitions)
        {
            foreach (Exhibition a in exhibitions)
            {
                if (this.Exhibitions.Count<Exhibition>() == 0 || !this.Exhibitions.Any(u => (u.Name == a.Name)))
                {
                    this.Exhibitions.Add(a);
                }
                else
                {
                    Console.WriteLine("This element is in table");
                }
                Console.WriteLine(a.Name);
            }
            this.SaveChanges();
        }

        public DbSet<Film> Films { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Favourites> Favourites { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Concert> Concerts { get; set; }

        public DbSet<Exhibition> Exhibitions { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
               .HasRequired<Film>(s => s.Film)
               .WithMany(g => g.Sessions)
               .HasForeignKey<int>(s => s.FilmId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .HasRequired<Cinema>(s => s.Cinema)
                .WithMany(g => g.Sessions)
                .HasForeignKey<int>(s => s.CinemaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cinema>()
                .Property(m => m.FavouritesId)
                .IsOptional();

            modelBuilder.Entity<Film>()
                .Property(m => m.FavouritesId)
                .IsOptional();

            modelBuilder.Entity<Concert>()
               .Property(m => m.FavouritesId)
               .IsOptional();

            modelBuilder.Entity<Exhibition>()
               .Property(m => m.FavouritesId)
               .IsOptional();
        }
    }
}
