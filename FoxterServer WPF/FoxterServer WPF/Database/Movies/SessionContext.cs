using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;

namespace FoxterServer_WPF
{
    public class SessionContext
    {
        public static SessionFromFile GetSession(string source)
        {
            SessionFromFile Session;

            XmlSerializer xs = new XmlSerializer(typeof(SessionFromFile));
            using (FileStream stream = new FileStream(source, FileMode.OpenOrCreate))
            {
                Session = (SessionFromFile)xs.Deserialize(stream);
            }

            return Session;
        }

        public static List<SessionFromFile> GetListOfSessions()
        {
            string filename;
            string extension = ".xml";
            List<SessionFromFile> sessionFromFiles = new List<SessionFromFile>();
            SessionFromFile session;
            for (int i = 0; true; i++)
            {
                filename = @"D:\Документы\Университет\4 семестр\ООТП\Курсовой\Parser\Parser\Session\";
                filename += i + extension;
                if (File.Exists(filename))
                {
                    session = SessionContext.GetSession(filename);
                }
                else
                {
                    break;
                }
                sessionFromFiles.Add(session);
            }
            return sessionFromFiles;
        }
    }
}