using System;
using System.Linq;
using System.Collections.Generic;
using H3IA9Z_ADT_2022_23_1_Client;
using Models;
using ConsoleTools;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);
            RestService rest = new RestService("http://localhost:18972");
            var MenuForVisitorsadmin = new ConsoleMenu()
                .Add("** READ By Id", () => ReadVisitorById(rest))
                .Add("** READ All", () => ReadAllVisitors(rest))
                .Add("** DELETE", () => DeleteVisitor(rest))
                .Add("** Best Visitor ", () => BestVisitor(rest))
                .Add("** Worst Visitor ", () => WorstVisitor(rest))
                .Add("** Reservations count  ", () => CountResers(rest))
                .Add("** GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "*** ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Yellow;
                });
            var MenuForVisitors = new ConsoleMenu()
                .Add("** CREATE", () => AddNewVisitor(rest))
                .Add("** Add Reservation", () => AddNewReservation(rest))
                .Add("** Read all Visitors", () => ReadAllVisitors(rest))
                .Add("** UpdateAddress", () => UpdateVisitorAddress(rest))
                .Add("** DELETE", () => DeleteVisitor(rest))
                .Add("** GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "*** ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Yellow;
                });
            var MenuForMovies = new ConsoleMenu()
                .Add("** CREATE", () => AddNewMovie(rest))
                .Add("** READ By Id", () => ReadMovieById(rest))
                .Add("** READ All", () => ReadAllMovie(rest))
                .Add("** UpdateCost", () => UpdateMoviecost(rest))
                .Add("** DELETE", () => DeleteMovie(rest))
                .Add("** Movies Earnings ", () => Movieearrings(rest))
                .Add("** Most Paid Movie ", () => MostPaidArt(rest))
                .Add("** Less Paid Movie ", () => LessPaidArt(rest))
                .Add("** GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "*** ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Yellow;
                });
            var MenuForReservations = new ConsoleMenu()
                .Add("** CREATE", () => AddNewReservation(rest))
                .Add("** READ By Id", () => ReadReservationById(rest))
                .Add("** READ All", () => ReadAllReservation(rest))
                .Add("** UpdateDate", () => UpdateReservationdate(rest))
                .Add("** DELETE", () => DeleteReservation(rest))
                .Add("** GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "*** ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Yellow;
                });
            var menuForAdministrator = new ConsoleMenu(args, level: 0)
                .Add("** Visitors", () => MenuForVisitorsadmin.Show())
                .Add("** Movies ", () => MenuForMovies.Show())
                .Add("** Reservations ", () => MenuForReservations.Show())
                .Add("** Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "*** ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Magenta;
                });
            var MainMenu = new ConsoleMenu(args, level: 0)
                .Add("** Visitor", () => MenuForVisitors.Show())
                .Add("** Manager ", () => menuForAdministrator.Show())
                .Add("** Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "*** ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Magenta;
                });

            MainMenu.Show();

        }

        #region visitorsMethods
        private static void AddNewVisitor(RestService rest)
        {
            try
            {
                Console.WriteLine("\n:: New Visitor ::\n");
                Console.Write("Visitor's Name : ");
                string name = Console.ReadLine();

                Console.Write("Visitor's Address : ");
                string address = Console.ReadLine();

                Console.Write("Visitor's Email : ");
                string email = Console.ReadLine();

                Console.Write("Visitor's Phone number : ");
                int phoneNumber = int.Parse(Console.ReadLine());

                Visitor vis = new Visitor() { Address = address, Email = email, Name = name, PhoneNumber = phoneNumber };

                rest.Post<Visitor>(vis, "visitor");

                Console.WriteLine("\n A vis with name " + name.ToString().ToUpper() + " has been added to the Database\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadVisitorById(RestService rest)
        {
            Console.Write("\n ID of Visitor :  ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10}  {"Address",5}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Visitor>(id, "visitor").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void CountResers(RestService rest)
        {
            Console.Write("Visitor's ID : ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Magenta;
                int coun = rest.Get<int>(id, "Noncrudvis/ReservationNUM");
                Console.WriteLine("This vis has : " + coun + " reservations.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadAllVisitors(RestService rest)
        {
            Console.WriteLine("\n   ALL Visitors :  \n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10} {"Address",5}");
            Console.ResetColor();
            var visitors = rest.Get<Visitor>("visitor");
            visitors.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateVisitorAddress(RestService rest)
        {
            Console.WriteLine("\n  Visitor's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New Address : ");
                string address = Console.ReadLine();
                Visitor vis1 = rest.Get<Visitor>(id, "visitor");
                vis1.Address = address;

                rest.Put<Visitor>(vis1, "visitor");


                Console.WriteLine("Address Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteVisitor(RestService rest)
        {
            Console.WriteLine("\n Visitor's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Visitor who will be deleted  has ID : " + id);
                rest.Delete(id, "visitor");
                Console.WriteLine("  Visitor deleted! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void BestVisitor(RestService rest)
        {
            var bestvisitors = rest.Get<KeyValuePair<int, int>>("Noncrudvis/BestVisitors");
            foreach (var item in bestvisitors)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Best Visitor Id : " + item.Key + ", Reservation number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void WorstVisitor(RestService rest)
        {
            var worstvisitors = rest.Get<KeyValuePair<int, int>>("Noncrudvis/WorstVisitors");
            foreach (var item in worstvisitors)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Worst Visitor Id : " + item.Key + ", Reservation number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion
        #region MovieMethods
        private static void AddNewMovie(RestService rest)
        {
            Console.WriteLine("\n:: New Movie ::\n");
            Console.Write("Artit's Name : ");
            string name = Console.ReadLine();

            Console.Write("Movie's Duration (hours) : ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Movie's price : ");
            int price = int.Parse(Console.ReadLine());

            Console.Write("Movie's category  : ");
            string category = Console.ReadLine();

            rest.Post<Movie>(new Movie() { Name = name, Duration = duration, Price = price, Category = category }, "movie");

            Console.WriteLine("\n A movie with the name  " + name.ToString().ToUpper() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadMovieById(RestService rest)
        {
            Console.WriteLine("\n ID of Movie :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Movie>(id, "movie").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllMovie(RestService rest)
        {
            Console.WriteLine("\n   ALL Movies :  \n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
            Console.ResetColor();
            var mvs = rest.Get<Movie>("movie");
            mvs.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateMoviecost(RestService rest)
        {
            Console.WriteLine("\n  Movie's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New Cost : ");
                int cost = int.Parse(Console.ReadLine());

                Movie art = rest.Get<Movie>(id, "movie");
                art.Price = cost;

                rest.Put<Movie>(art, "movie");


                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteMovie(RestService rest)
        {
            Console.WriteLine("\n Movie's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Movie who will be deleted has ID :  " + id);
                rest.Delete(id, "movie");
                Console.WriteLine("  Movie deleted! ");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void Movieearrings(RestService rest)
        {
            var moviesearnings = rest.Get<KeyValuePair<string, int>>("Noncrudmovie/MovieEarnings");
            foreach (var item in moviesearnings)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("MOVIE NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void MostPaidArt(RestService rest)
        {
            var Mostpaidmov = rest.Get<KeyValuePair<string, int>>("Noncrudmovie/Mostpaidmov");
            foreach (var item in Mostpaidmov)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("MOVIE NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void LessPaidArt(RestService rest)
        {
            var Lesspaidmov = rest.Get<KeyValuePair<string, int>>("Noncrudmovie/Lesspaidmov");
            foreach (var item in Lesspaidmov)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("MOVIE NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion
        #region ReservationMethods
        private static void AddNewReservation(RestService rest)
        {
            Console.WriteLine("\n:: New Reservation ::\n");


            Console.Write("Vis ID  : ");
            int visId = int.Parse(Console.ReadLine());

            Console.Write("Movie ID : : ");
            int movieId = int.Parse(Console.ReadLine());

            Console.Write(" Date [yyyy-MM-dd HH:mm] : ");
            DateTime dateTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
            try
            {
                rest.Post<Reservation>(new Reservation() { VisitorId = visId, MovieId = movieId, DateTime = dateTime }, "reservations");
                Console.WriteLine("\n A Reservation with For visitor with ID " + visId + " has been added to the Database\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadReservationById(RestService rest)
        {
            Console.WriteLine("\n ID of Reservations :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n{"Id",3} | {"Vis Id ",-20} {"DateTime",10} {"Movie Id",25}");
                Console.ResetColor();
                var re = rest.Get<Reservation>(id, "reservations");
                Console.WriteLine(re.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllReservation(RestService rest)
        {
            Console.WriteLine("\n   ALL Reservations :  \n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n{"Id",3} | {"Vis Id ",-20} {"DateTime",10} {"Movie Id",25}");
            Console.ResetColor();
            var reservations = rest.Get<Reservation>("reservations");
            reservations.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateReservationdate(RestService rest)
        {
            Console.WriteLine("\n  Reservation's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());

                Console.Write("\n New Date [yyyy - MM - dd HH: mm] :  ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
                Reservation r1 = rest.Get<Reservation>(id, "reservations");
                r1.DateTime = date;

                rest.Put<Reservation>(r1, "reservations");


                Console.WriteLine("Date Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteReservation(RestService rest)
        {
            Console.WriteLine("Reservation's ID which will be deleted ");

            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "reservations");
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion
    }
}
