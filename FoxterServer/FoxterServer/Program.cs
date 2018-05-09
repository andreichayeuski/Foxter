using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace FoxsterServer
{
    class Program
    {
        private static async Task ConnectWithDB()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Connection is open");
            }
            Console.WriteLine("Connection is closed...");
        }

        private static async Task ReadDataAsync()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Connection is open");

                SqlCommand command = new SqlCommand
                {
                    CommandText = "select * from Cinema",
                    Connection = connection
                };
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", dataReader.GetName(0),
                        dataReader.GetName(1), dataReader.GetName(2), dataReader.GetName(3),
                        dataReader.GetName(4), dataReader.GetName(5), dataReader.GetName(6),
                        dataReader.GetName(7), dataReader.GetName(8));
                    while (await dataReader.ReadAsync()) // построчно считываем данные
                    {
                        object id = dataReader.GetValue(0);
                        object name = dataReader.GetValue(1);
                        object latitude = dataReader.GetValue(2);
                        object longitude = dataReader.GetValue(3);
                        object info = dataReader.GetValue(4);
                        object image = dataReader.GetValue(5);
                        object video = dataReader.GetValue(6);
                        object mainphoto = dataReader.GetValue(7);
                        object time = dataReader.GetString(8);
                        string sub = "***";
                        Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7} \t{8}", id, name, latitude ?? sub,
                            longitude ?? sub, info ?? sub, image ?? sub,
                            video ?? sub, mainphoto, time);
                    }
                }

                dataReader.Close();
            }

        }

        static void Main(string[] args)
        {
            try
            {
                //ReadDataAsync().GetAwaiter();
                //ConnectWithDB().GetAwaiter();
                
                using (FilmContext filmContext = new FilmContext())
                {
                    Console.WriteLine("Start searching info");
                    List<Film> films = FilmContext.GetListOfFilms();

                    Console.WriteLine("Work with database");
                    //FilmContext.CheckAndUpdateFilmInTable(filmContext, films);
                    Console.WriteLine("Elements from database");
                    foreach (Film a in filmContext.Films)
                    {
                        Console.WriteLine(a.Id);
                    }
                    AsyncServ.SetContext(filmContext);
                    AsyncServ.StartListening();
                }

                Console.WriteLine("Waiting for key");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.InnerException + " " + ex.Message + "\n\n");
            }
            finally
            {

            }
            Console.ReadKey();
        }
    }
}