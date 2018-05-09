using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
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
    }
}
