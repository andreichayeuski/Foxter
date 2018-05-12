using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
namespace ClassLibrary
{
    public class FilmHandler
    {
        public static byte[] ConvertFilmListToByteArray(List<Film> list)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, list);
            return stream.ToArray();
        }

        public static List<Film> ConvertByteArrayToFilmList(byte[] bytearray)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            List<Film> result = new List<Film>();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (List<Film>)bf.Deserialize(stream);
            return result;
        }

        public static List<string> GetListOfImages(string str)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex("[А-Я][а-я]+");
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach(string s in matches)
                {
                    list.Add(s);
                }
            }
            return list;
        }

        public static List<string> GetListOfGenres(string str)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex("(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))");
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (string s in matches)
                {
                    list.Add(s);
                }
            }
            return list;
        }
    }
}
