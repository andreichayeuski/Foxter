using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows;
using ClassLibrary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

namespace CP_WPF
{
    public class AsyncClient
    {
        public static void SetTypeInfo(TypeOfInfo src)
        {
            TypeOfTheInfo = src;
        }

        public static void SetUser(User src)
        {
            user = src;
        }

        public static void SetFavourite(Favourites src) // посмотри файл регистрации, где юзер заносится сюда и вызывается сервер,
                                                        // тут аналогично будет с избранным, и так лучше делать в конце работы с избранным, когда оно заполнено
        {
            favourite = src;
        }

        public static void SetComment(Comment src)
        {
            comment = src;
        }

        public static volatile TypeOfInfo TypeOfTheInfo;
        private const int port = 11000;

        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public static volatile bool flagUser = true;
        public static volatile bool flagFavourite = true;
        public static volatile bool flagComment = true;

        public static List<Film> films;
        public static List<Cinema> cinemas;
        public static List<User> users;
        public static List<Favourites> favourites;
        public static List<Concert> concerts;
        public static List<Exhibition> exhibitions;
        public static List<Comment> comments;

        public static User user;
        public static Favourites favourite;
        public static Comment comment;

        public static void StartClient()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry("Client1");
                IPAddress ipAddress = ipHostInfo.AddressList[1];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                if (TypeOfTheInfo == TypeOfInfo.User && !flagUser)
                {
                    flagUser = true;
                    Send(client, user);
                    TypeOfTheInfo = TypeOfInfo.Default;
                }
                else if (TypeOfTheInfo == TypeOfInfo.Favourite && !flagFavourite)
                {
                    flagFavourite = true;
                    Send(client, favourite);
                    TypeOfTheInfo = TypeOfInfo.Default;
                }
                else if (TypeOfTheInfo == TypeOfInfo.Comment && !flagComment)
                {
                    flagComment = true;
                    Send(client, comment);
                    TypeOfTheInfo = TypeOfInfo.Default;
                }
                else
                {
                    Send(client, TypeOfTheInfo.ToString());
                }
                sendDone.WaitOne();

                Receive(client);
                receiveDone.WaitOne();

                client.Shutdown(SocketShutdown.Both);
                client.Close();

                connectDone.Reset();
                sendDone.Reset();
                receiveDone.Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application application = Application.Current;
                application.Shutdown();
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);

                connectDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application application = Application.Current;
                application.Shutdown();
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject
                {
                    workSocket = client
                };

                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application application = Application.Current;
                application.Shutdown();
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (TypeOfTheInfo == TypeOfInfo.User)
                {
                    ar.AsyncWaitHandle.WaitOne(2000);
                }
                if (TypeOfTheInfo == TypeOfInfo.Comment)
                {
                    ar.AsyncWaitHandle.WaitOne(2000);
                }
                StateObject state = (StateObject)ar.AsyncState;

                Socket client = state.workSocket;

                client.EndReceive(ar);

                switch (TypeOfTheInfo)
                {
                    case TypeOfInfo.Films:
                        {
                            films = FilmHandler.ConvertByteArrayToFilmList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.Cinemas:
                        {
                            cinemas = CinemaHandler.ConvertByteArrayToCinemaList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.Users:
                        {
                            users = UserHandler.ConvertByteArrayToUserList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.User:
                        {
                            flagUser = false;
                            break;
                        }
                    case TypeOfInfo.Favourites:
                        {
                            favourites = FavouritesHandler.ConvertByteArrayToFavouritesList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.Concerts:
                        {
                            concerts = ConcertHandler.ConvertByteArrayToConcertList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.Exhibitions:
                        {
                            exhibitions = ExhibitionHandler.ConvertByteArrayToExhibitionList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.Comments:
                        {
                            comments = CommentHandler.ConvertByteArrayToCommentList(state.buffer);
                            break;
                        }
                    case TypeOfInfo.Favourite:
                        {
                            flagFavourite = false;
                            break;
                        }
                    case TypeOfInfo.Comment:
                        {

                            flagComment = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                receiveDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application application = Application.Current;
                application.Shutdown();
            }
        }

        private static void Send(Socket client, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void Send(Socket client, User data)
        {
            byte[] byteData = UserHandler.ConvertUserToByteArray(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void Send(Socket client, Favourites data)
        {
            byte[] byteData = FavouritesHandler.ConvertFavouritesToByteArray(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void Send(Socket client, Comment data)
        {
            byte[] byteData = CommentHandler.ConvertCommentToByteArray(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);

                sendDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application application = Application.Current;
                application.Shutdown();
            }
        }
    }
}
