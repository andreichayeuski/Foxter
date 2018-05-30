using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;
using System;
using System.Windows;

namespace FoxterServer_WPF
{
    public class ConcertContext
    {
        public static Concert GetConcert(string source)
        {
            try
            {
                Concert concert;

                XmlSerializer xs = new XmlSerializer(typeof(Concert));
                using (FileStream stream = new FileStream(source, FileMode.OpenOrCreate))
                {
                    concert = (Concert)xs.Deserialize(stream);
                }

                return concert;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static List<Concert> GetListOfConcerts()
        {
            string filename;
            string extension = ".xml";
            List<Concert> concerts = new List<Concert>();

            for (int i = 0; true; i++)
            {
                Concert concert;
                filename = @"D:\Документы\Университет\4 семестр\ООТП\Курсовой\Parser\Parser\Concert\";
                filename += i + extension;
                if (File.Exists(filename))
                {
                    concert = ConcertContext.GetConcert(filename);
                }
                else
                {
                    break;
                }
                concerts.Add(concert);
            }
            return concerts;
        }
    }
}
