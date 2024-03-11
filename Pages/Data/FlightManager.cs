using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Components.Pages.Data
{
    internal class FlightManager
    {

        public static string WEEKDAY_ANY = "Any";

        public static string WEEKDAY_SUNDAY = "Sunday";

        public static string WEEKDAY_MONDAY = "Monday";

        public static string WEEKDAY_TUESDAY = "Tuesday";

        public static string WEEKDAY_WEDNESDAY = "Wednesday";

        public static string WEEKDAY_THURSDAY = "Thursday";

        public static string WEEKDAY_FRIDAY = "Friday";

        public static string WEEKDAY_SATURDAY = "Saturday";

        public static string FLIGHTS_TEXT = Path.Combine("Resources", "Files", "flights.csv");

        public static string AIRPORTS_TEXT = Path.Combine("Resources", "Files", "airports.csv");


        public static List<Flight> flights = new List<Flight>();
        public static List<string> airports = new List<string>();

        public FlightManager()
        {
            populateAirports();
            populateFlights();
        }


        public List<string> getAirports()
        {
            return airports;
        }


        public static List<Flight> getFlights()
        {
            return flights;
        }


        public string findAirportByCode(string code)
        {
            foreach (string airport in airports)
            {
                if (airport.Equals(code))
                    return airport;
            }

            return null;
        }


        public static Flight findFlightByCode(string code)
        {
            foreach (Flight flight in flights)
            {
                if (flight.Code.Equals(code))
                    return flight;
            }

            return null;
        }


        public static List<Flight> findFlights(string from, string to, string weekday)
        {
            List<Flight> found = new List<Flight>();

            foreach (Flight flight in flights)
            {
                if ((weekday.Equals(WEEKDAY_ANY) || flight.Weekday.Equals(weekday)) &&
                    (to.Equals(WEEKDAY_ANY) || flight.To.Equals(to)) &&
                    (from.Equals(WEEKDAY_ANY) || flight.From.Equals(from)))
                {
                    found.Add(flight);
                }
            }

            return found;
        }



        private void populateFlights()
        {
            flights.Clear();
            try
            {
                int counter = 0;
                Flight flight;
  
                foreach (string line in File.ReadLines(FLIGHTS_TEXT))
                {
                    Console.WriteLine(line);

                    string[] parts = line.Split(",");

                    string code = parts[0];
                    string airline = parts[1];
                    string from = parts[2];
                    string to = parts[3];
                    string weekday = parts[4];
                    string time = parts[5];
                    int seatsAvailable = short.Parse(parts[6]);
                    double pricePerSeat = double.Parse(parts[7]);
                    string fromAirport = findAirportByCode(from);
                    string toAirport = findAirportByCode(to);

                    try
                    {
                        flight = new Flight(code, airline, fromAirport, toAirport, weekday, time, seatsAvailable, pricePerSeat);

                        flights.Add(flight);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error processing line {counter + 1}: {e.Message}");
                    }

                    counter++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading flights file: {e.Message}");
            }
        }


        private void populateAirports()
        {
            try
            {
                int counter = 0;
                foreach (string line in File.ReadLines(AIRPORTS_TEXT))
                {
                    string[] parts = line.Split(",");

                    string code = parts[0];
                    string name = parts[1];
                    airports.Add(code);

                    counter++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading airports file: {e.Message}");
            }
        } 
    }
}
