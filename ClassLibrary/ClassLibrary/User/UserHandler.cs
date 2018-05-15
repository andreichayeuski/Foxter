using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibrary
{
    public class UserHandler
    {
        public static byte[] ConvertUserListToByteArray(List<User> list)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, list);
            return stream.ToArray();
        }

        public static List<User> ConvertByteArrayToUserList(byte[] bytearray)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            List<User> result = new List<User>();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (List<User>)bf.Deserialize(stream);
            return result;
        }

        public static User ConvertByteArrayToUser(byte[] bytearray)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            User result = new User();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (User)bf.Deserialize(stream);
            return result;
        }

        public static byte[] ConvertUserToByteArray(User user)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, user);
            return stream.ToArray();
        }
    }
}
