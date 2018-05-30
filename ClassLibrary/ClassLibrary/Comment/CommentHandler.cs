using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibrary
{
    public class CommentHandler
    {
        public static byte[] ConvertCommentListToByteArray(List<Comment> list)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, list);
            return stream.ToArray();
        }

        public static List<Comment> ConvertByteArrayToCommentList(byte[] bytearray)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            List<Comment> result = new List<Comment>();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (List<Comment>)bf.Deserialize(stream);
            return result;
        }

        public static Comment ConvertByteArrayToComment(byte[] bytearray)
        {
            Comment comment = new Comment();
            using (MemoryStream m = new MemoryStream(bytearray))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    comment.Id = reader.ReadInt32();
                    comment.UserName = reader.ReadString();
                    comment.Date = reader.ReadString();
                    comment.Place = reader.ReadString();
                    comment.Text = reader.ReadString();
                    comment.Theme = reader.ReadString();
                }
            }
            return comment;

            /*MemoryStream stream = new MemoryStream();
            BinaryWriter binaryFormatter = new BinaryWriter(stream);
            Comment result = new Comment();
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bf = new BinaryFormatter();
            result = (Comment)bf.Deserialize(stream);
            return result;*/
        }

        public static byte[] ConvertCommentToByteArray(Comment comment)
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(comment.Id);
                    writer.Write(comment.UserName);
                    writer.Write(comment.Date);
                    writer.Write(comment.Place);
                    writer.Write(comment.Text);
                    writer.Write(comment.Theme);
                }
                return m.ToArray();
            }
            /*MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, comment);
            return stream.ToArray();*/
        }
    }
}
