using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ClassLibrary;
using System.Data.Entity;

namespace FoxsterServer
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

        public static void MakeListOfSessionAndAddToDatabase(List<SessionFromFile> source, FilmContext filmContext)
        {
            List<Cinema> cinema_list = new List<Cinema>();
            List<Session> result = new List<Session>();
            cinema_list.AddRange(filmContext.Cinemas);
            foreach (SessionFromFile s in source)
            {
                for (int i = 0; i <= cinema_list.Count; i++)
                {
                    foreach (Film f in filmContext.Films)
                    {
                        if (f.Link == s.FilmLink && cinema_list[i].Link == s.CinemaLink)
                        {
                            Session session = new Session()
                            {
                                Cinema = cinema_list[i],
                                Film = f,
                                Date = s.Date,
                                Time = s.Time
                            };
                            result.Add(session);
                            f.Sessions.Add(session);
                            filmContext.Sessions.Add(session);
                        }
                    }
                    filmContext.Cinemas.Find(cinema_list[i].Id).Sessions.AddRange(result);
                    result.Clear();
                }
            }
            filmContext.SaveChanges();
        }
    }
}