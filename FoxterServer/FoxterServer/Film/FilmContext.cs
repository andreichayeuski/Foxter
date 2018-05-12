using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;

namespace FoxsterServer
{
    class FilmContext : DbContext
    {
        public FilmContext() :
            base("FilmsDB")
        {

        }
        public static Film GetFilm(string source)
        {
            Film film;

            XmlSerializer xs = new XmlSerializer(typeof(Film));
            using (FileStream stream = new FileStream(source, FileMode.OpenOrCreate))
            {
                film = (Film)xs.Deserialize(stream);
            }

            return film;
        }

        public static List<Film> GetListOfFilms()
        {
            string filename;
            string extension = ".xml";
            List<Film> films = new List<Film>();

            for (int i = 0; i < 20; i++)
            {
                filename = @"D:\Документы\Университет\4 семестр\ООТП\Курсовой\Parser\Parser\";
                filename += i + extension;
                Film film = FilmContext.GetFilm(filename);
                films.Add(film);
            }
            return films;
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

        public DbSet<Film> Films { get; set; }
    }
}
