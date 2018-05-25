using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;

namespace FoxterServer_WPF
{
    public class FilmContext
    {
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

            for (int i = 0; true; i++)
            {
                Film film;
                filename = @"D:\Документы\Университет\4 семестр\ООТП\Курсовой\Parser\Parser\Film\";
                filename += i + extension;
                if (File.Exists(filename))
                {
                    film = FilmContext.GetFilm(filename);
                }
                else
                {
                    break;
                }
                films.Add(film);
            }
            return films;
        }
    }
}