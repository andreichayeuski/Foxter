using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ClassLibrary;
using System.Threading.Tasks;
using System.Windows;
public enum TypeOfInfo
{
    Default = 0, Films = 1, Cinemas, Users, Favourites, Comments, Concerts, Exhibitions, User, Favourite, Comment
};

public class StateObject
{
    public Socket workSocket = null;

    public const int BufferSize = 500000;

    public byte[] buffer = new byte[BufferSize];

    public StringBuilder sb = new StringBuilder();
}

namespace FoxterServer_WPF
{
    public class AsyncServ
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static void SetTypeInfo(TypeOfInfo src)
        {
            TypeOfTheInfo = src;
        }

        public static void SetContext(FoxterContext context)
        {
            foxterContext = context;
        }

        public static volatile FoxterContext foxterContext;
        
        public AsyncServ()
        {

        }

        public static volatile bool ServerIsOn = false;

        public static TypeOfInfo TypeOfTheInfo;

        public static void StopListening()
        {
            ServerIsOn = false;
        }

        public static void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            ServerIsOn = true;
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (ServerIsOn)
                {
                    allDone.Reset();

                    MessageBox.Show("Waiting for a connection...\n");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                allDone.Set();

                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                StateObject state = new StateObject
                {
                    workSocket = handler
                };
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                string content = String.Empty;

                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                int bytesRead = handler.EndReceive(ar);

                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                content = state.sb.ToString();

                if (content.Contains("Films"))
                {
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Film> list = new List<Film>();
                    list.AddRange(foxterContext.Films);
                    Send(handler, list);
                }
                else if (content.Contains("Cinemas"))
                {
                    List<Cinema> list = new List<Cinema>();
                    list.AddRange(foxterContext.Cinemas);
                    Send(handler, list);
                }
                else if (content.Contains("Users"))
                {
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<User> list = new List<User>();
                    list.AddRange(foxterContext.Users);
                    Send(handler, list);
                }
                else if (TypeOfTheInfo == TypeOfInfo.User)
                {
                    foxterContext.Users.Add(UserHandler.ConvertByteArrayToUser(state.buffer));
                    foxterContext.SaveChanges();
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<User> list = new List<User>();
                    list.AddRange(foxterContext.Users);
                    Send(handler, list);
                }
                else if (content.Contains("User"))
                {
                    TypeOfTheInfo = TypeOfInfo.User;
                    Send(handler, "Start adding to database");
                }
                else if (content.Contains("Favourites"))
                {
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Favourites> list = new List<Favourites>();
                    list.AddRange(foxterContext.Favourites);
                    Send(handler, list);
                }
                else if (TypeOfTheInfo == TypeOfInfo.Favourite)
                {
                    Favourites favourite = FavouritesHandler.ConvertByteArrayToFavourites(state.buffer);
                    if (foxterContext.Favourites.Find(favourite) != null)
                    {
                        foxterContext.Favourites.Find(favourite).Cinemas = favourite.Cinemas;
                        foxterContext.Favourites.Find(favourite).Films = favourite.Films;
                    }
                    else
                    {
                        foxterContext.Favourites.Add(favourite);
                    }
                    foxterContext.SaveChanges();
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Favourites> list = new List<Favourites>();
                    list.AddRange(foxterContext.Favourites);
                    Send(handler, list);
                }
                else if (content.Contains("Favourite"))
                {
                    TypeOfTheInfo = TypeOfInfo.Favourite;
                    Send(handler, "Start adding to database");
                }
                else if (content.Contains("Concerts"))
                {
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Concert> list = new List<Concert>();
                    list.AddRange(foxterContext.Concerts);
                    Send(handler, list);
                }
                else if (content.Contains("Exhibitions"))
                {
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Exhibition> list = new List<Exhibition>();
                    list.AddRange(foxterContext.Exhibitions);
                    Send(handler, list);
                }
                else if (content.Contains("Comments"))
                {
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Comment> list = new List<Comment>();
                    list.AddRange(foxterContext.Comments);
                    Send(handler, list);
                }
                else if (TypeOfTheInfo == TypeOfInfo.Comment)
                {
                    Comment comment = CommentHandler.ConvertByteArrayToComment(state.buffer);
                    foxterContext.Comments.Add(comment);
                    foxterContext.SaveChanges();
                    TypeOfTheInfo = TypeOfInfo.Default;
                    List<Comment> list = new List<Comment>();
                    list.AddRange(foxterContext.Comments);
                    Send(handler, list);
                }
                else if (content.Contains("Comment"))
                {
                    TypeOfTheInfo = TypeOfInfo.Comment;
                    Send(handler, "Start adding to database");
                }
                else
                {
                    MessageBox.Show("Error in data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\n" + ex.Message);
            }
        }

        public static void Send(Socket handler, List<Film> data)
        {
            byte[] byteData = FilmHandler.ConvertFilmListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, List<Cinema> data)
        {
            byte[] byteData = CinemaHandler.ConvertCinemaListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, List<User> data)
        {
            byte[] byteData = UserHandler.ConvertUserListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, List<Favourites> data)
        {
            byte[] byteData = FavouritesHandler.ConvertFavouritesListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, List<Concert> data)
        {
            byte[] byteData = ConcertHandler.ConvertConcertListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, List<Exhibition> data)
        {
            byte[] byteData = ExhibitionHandler.ConvertExhibitionListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, List<Comment> data)
        {
            byte[] byteData = CommentHandler.ConvertCommentListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;

                int bytesSent = handler.EndSend(ar);
                MessageBox.Show("Server send " + bytesSent + " bytes to client");
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}