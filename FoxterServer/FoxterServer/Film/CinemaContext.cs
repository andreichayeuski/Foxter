using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;

namespace FoxsterServer
{
    public class CinemaContext
    {
        public static Cinema GetCinema(string source)
        {
            Cinema cinema;

            XmlSerializer xs = new XmlSerializer(typeof(Cinema));
            using (FileStream stream = new FileStream(source, FileMode.OpenOrCreate))
            {
                cinema = (Cinema)xs.Deserialize(stream);
            }

            return cinema;
        }

        public static List<Cinema> GetListOfCinemas()
        {
            string filename;
            string extension = ".xml";
            List<Cinema> cinemas = new List<Cinema>();
            Cinema cinema;
            for (int i = 0; true; i++)
            {
                filename = @"D:\Документы\Университет\4 семестр\ООТП\Курсовой\Parser\Parser\Cinema\";
                filename += i + extension;
                if (File.Exists(filename))
                {
                    cinema = CinemaContext.GetCinema(filename);
                }
                else
                {
                    break;
                }
                cinemas.Add(cinema);
            }
            return cinemas;
        }
    }
}
