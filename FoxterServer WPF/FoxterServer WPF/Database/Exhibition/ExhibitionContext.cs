using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;
using System;
using System.Windows;
namespace FoxterServer_WPF
{
    public class ExhibitionContext
    {
        public static Exhibition GetExhibition(string source)
        {
            try
            {
                Exhibition exhibition;

                XmlSerializer xs = new XmlSerializer(typeof(Exhibition));
                using (FileStream stream = new FileStream(source, FileMode.OpenOrCreate))
                {
                    exhibition = (Exhibition)xs.Deserialize(stream);
                }

                return exhibition;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static List<Exhibition> GetListOfExhibitions()
        {
            string filename;
            string extension = ".xml";
            List<Exhibition> exhibitions = new List<Exhibition>();

            for (int i = 0; true; i++)
            {
                Exhibition exhibition;
                filename = @"D:\Документы\Университет\4 семестр\ООТП\Курсовой\Parser\Parser\Exhibition\";
                filename += i + extension;
                if (File.Exists(filename))
                {
                    exhibition = ExhibitionContext.GetExhibition(filename);
                }
                else
                {
                    break;
                }
                exhibitions.Add(exhibition);
            }
            return exhibitions;
        }
    }
}
