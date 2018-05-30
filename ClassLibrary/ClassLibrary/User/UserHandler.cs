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
            /*User user = new User();
            using (MemoryStream m = new MemoryStream(bytearray))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    user.Id = reader.ReadInt32();
                    user.UserName = reader.ReadString();
                    user.Password = reader.ReadString();
                    BinaryFormatter bf = new BinaryFormatter();
                    user.Favourites = (Favourites)bf.Deserialize(m);
                }
            }
            return user;*/
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
            /*using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(user.Id);
                    writer.Write(user.UserName);
                    writer.Write(user.Password);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(m, user.Favourites);
                }
                return m.ToArray();
            }*/
        }
    }
}
