using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ClassLibrary;
public class StateObject
{
    // Client  socket.  
    public Socket workSocket = null;
    // Size of receive buffer
    // Размер буфера приёма 
    public const int BufferSize = 500000;
    // Receive buffer
    // Буфер приема
    public byte[] buffer = new byte[BufferSize];
    // Received data string  
    // Полученная строка данных
    public StringBuilder sb = new StringBuilder();
}

namespace FoxsterServer
{
    public enum TypeOfInfo
    {
        Film = 1
    };
    class AsyncServ
    {
        // Thread signal
        // Сигнал потока
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static void SetTypeInfo(TypeOfInfo src)
        {
            TypeOfTheInfo = src;
        }

        public static void SetContext(FilmContext context)
        {
            filmContext = context;
        }

        public static FilmContext filmContext;


        public AsyncServ()
        {

        }

        public static TypeOfInfo TypeOfTheInfo;

        public static void StartListening()
        {
            // Establish the local endpoint for the socket 
            // Установите локальную конечную точку для сокета  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections
            // Привяжите гнездо к локальной конечной точке и прослушайте входящие соединения
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    // Установите событие в состояние несоответствия.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections. 
                    // запустите асинхронный сокет для прослушивания соединений
                    Console.WriteLine("Waiting for a connection...");
                    //if (Console.ReadKey().Key == ConsoleKey.Q)
                    //{
                    //    break;
                    //}
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    // Подождите, пока соединение не будет выполнено до продолжения.
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // Signal the main thread to continue.  
                // Сигнал основного потока для продолжения
                allDone.Set();

                // Get the socket that handles the client request.
                // Получите сокет, обрабатывающий запрос клиента.
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                // Создайте объект состояния.
                StateObject state = new StateObject
                {
                    workSocket = handler
                };
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            string content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            // Получить объект состояния и гнездо обработчика
            // от объекта асинхронного состояния.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.  
            // Чтение данных из клиентского сокета.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                // Может быть больше данных, поэтому сохраните полученные данные до сих пор.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));
                List<Film> list = new List<Film>();
                foreach(Film f in filmContext.Films)
                {
                    list.Add(f);
                }
                content = state.sb.ToString();
                switch (content)
                {
                    case "Film":
                        {
                            Send(handler, list);
                            break;
                        }
                    default:
                        {
                            Send(handler, "Nothing to show");
                            break;
                        }
                }
            }
            else
            {
                // Not all data received. Get more.  
                // Не все полученные данные. Получите больше.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }

        public static void Send(Socket handler, List<Film> data)
        {
            byte[] byteData = FilmHandler.ConvertFilmListToByteArray(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0,
               new AsyncCallback(SendCallback), handler);
        }

        public static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            // Преобразование строковых данных в байтовые данные с использованием кодировки ASCII.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            // Начните отправку данных на удаленное устройство.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                // Извлеките сокет из объекта состояния.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                // Полная отправка данных на удаленное устройство.
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