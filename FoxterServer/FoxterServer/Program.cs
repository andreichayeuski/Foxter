using System;
using ClassLibrary;
using System.Collections.Generic;

namespace FoxsterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (FilmContext filmContext = new FilmContext())
                {
                    start: Console.WriteLine("\nPlease, select the operating mode:\n1 - Update information in database;\n" +
                        "2 - Start waiting for connections\n" + 
                        "Q - Exit");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            {
                                Console.WriteLine("The update mode is selected");
                                Console.WriteLine("Start searching info");
                                List<Film> films = FilmContext.GetListOfFilms();
                                List<Cinema> cinemas = CinemaContext.GetListOfCinemas();
                                Console.WriteLine("Work with database");
                                filmContext.CheckAndUpdateFilmInTable(films);
                                filmContext.CheckAndUpdateCinemaInTable(cinemas);
                                goto start;
                            }
                        case ConsoleKey.D2:
                            {
                                Console.WriteLine("The listening mode is selected");
                                Console.WriteLine("Elements from database");
                                Console.WriteLine("Films");
                                foreach (Film a in filmContext.Films)
                                {
                                    Console.WriteLine(a.Name);
                                }
                                Console.WriteLine("Cinemas");
                                foreach (Cinema a in filmContext.Cinemas)
                                {
                                    Console.WriteLine(a.Name);
                                }
                                AsyncServ.SetContext(filmContext);
                                AsyncServ.StartListening();
                                break;
                            }
                        case ConsoleKey.Q:
                            {
                                Console.WriteLine("Exit from server");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Try again");
                                goto start;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.InnerException + " " + ex.Message + "\n\n");
            }
            finally
            {
                Console.WriteLine("Waiting for key...");
            }
            Console.ReadKey(true);
        }
    }
}