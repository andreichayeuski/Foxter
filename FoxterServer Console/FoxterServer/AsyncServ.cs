using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ClassLibrary;

public enum TypeOfInfo
{
    Default = 0, Films = 1, Cinemas, Users, User
};

public class StateObject
{
    public Socket workSocket = null;

    public const int BufferSize = 500000;

    public byte[] buffer = new byte[BufferSize];

    public StringBuilder sb = new StringBuilder();
}

namespace FoxsterServer
{
    class AsyncServ
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static void SetTypeInfo(TypeOfInfo src)
        {
            TypeOfTheInfo = src;
        }

        public static void SetContext(FilmContext context)
        {
            filmContext = context;
        }

        public static volatile FilmContext filmContext;

        public AsyncServ()
        {

        }

        public static TypeOfInfo TypeOfTheInfo;

        public static void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    allDone.Reset();

                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
            finally
            {

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
                    List<Film> list = new List<Film>();
                    foreach (Film f in filmContext.Films)
                    {
                        list.Add(f);
                    }
                    Send(handler, list);
                }
                else if (content.Contains("Cinemas"))
                {
                    List<Cinema> list = new List<Cinema>();
                    foreach (Cinema c in filmContext.Cinemas)
                    {
                        list.Add(c);
                    }
                    Send(handler, list);
                }
                else if (content.Contains("Users"))
                {
                    List<User> list = new List<User>();
                    foreach (User u in filmContext.Users)
                    {
                        list.Add(u);
                    }
                    Send(handler, list);
                }
                else if (TypeOfTheInfo == TypeOfInfo.User)
                {
                    filmContext.Users.Add(UserHandler.ConvertByteArrayToUser(state.buffer));
                    filmContext.SaveChanges();
                    TypeOfTheInfo = TypeOfInfo.Default;
                    Send(handler, "User was added to database");
                }
                else if (content.Contains("User"))
                {
                    TypeOfTheInfo = TypeOfInfo.User;
                    Send(handler, "Start adding to database");
                }
                else
                {
                    Console.WriteLine("Error in data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!\n" + ex.Message);
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
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}