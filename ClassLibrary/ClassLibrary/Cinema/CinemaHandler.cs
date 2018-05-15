using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class CinemaHandler
    {
        public static byte[] ConvertCinemaListToByteArray(List<Cinema> list)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, list);
            return stream.ToArray();
        }

        public static List<Cinema> ConvertByteArrayToCinemaList(byte[] bytearray)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            List<Cinema> result = new List<Cinema>();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (List<Cinema>)bf.Deserialize(stream);
            return result;
        }

        public static List<string> GetListOfImages(string str)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex("(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))");
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match s in matches)
                {
                    list.Add(s.Value);
                }
            }
            return list;
        }
    }
}
