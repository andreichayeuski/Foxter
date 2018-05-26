using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibrary
{
    public class FavouritesHandler
    {
        public static byte[] ConvertFavouritesListToByteArray(List<Favourites> list)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, list);
            return stream.ToArray();
        }

        public static List<Favourites> ConvertByteArrayToFavouritesList(byte[] bytearray)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            List<Favourites> result = new List<Favourites>();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (List<Favourites>)bf.Deserialize(stream);
            return result;
        }
    }
}
