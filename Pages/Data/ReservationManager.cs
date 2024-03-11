using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Components.Pages.Data
{
    internal class ReservationManager
    {

        /**
         * The location of the reservation file.
         */
        private static string Reservation_TXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\reservation.csv");

        private static Random random = new Random();
        /**
         * Holds the Reservation objects.
         */
        private static List<Reservation> reservations = new List<Reservation>();

        /**
         * Finds reservations containing either reservation code or airline.
         * @param reservationCode Reservation code to search for.
         * @param airline Airline to search for.
         * @param name Travelers name to search for.
         * @return Any matching Reservation objects.
         */
        public List<Reservation> FindReservations(string reservationCode, string airline, string name)
        {
            List<Reservation> found = new List<Reservation>();

            foreach (Reservation reservation in reservations)
            {
                if (reservation.Code.Contains(reservationCode) && reservation.Airline.Contains(airline) && reservation.Name.Contains(name))
                {
                    found.Add(reservation);
                }
                else if (reservation.Code.Contains(reservationCode))
                {
                    found.Add(reservation);
                }
                // TODO
                // add a case to get reservation by Name   
                // add a case to get reservation by Airline   
                // ...................................
                // done by Ranjit on 5th March

                // added code for individual criteras such as Airline and Name which was missing earlier.
                // if the condition is fulfilled the it is added to found list.
                else if (reservation.Airline.Contains(airline))
                {
                    found.Add(reservation);
                }

                else if (reservation.Name.Contains(name))
                {
                    found.Add(reservation);
                }
                
            }

            return found;
        }

        public string GenerateResCode()
        {
            return GenerateReservationCode();
        }

        /**
         * Gets reservation code using a flight.
         * @param flight Flight instance.
         * @return Reservation code.
         */
        public string GenerateReservationCode()
        {           
            string reservationCode;

            do
            {
                char letter = (char)('A' + random.Next(26));
                string numbers = random.Next(1000, 10000).ToString();
                reservationCode = letter + numbers;
            } while (IsCodeGenerated(reservationCode, Reservation_TXT));

            return reservationCode;
        }

        private static bool IsCodeGenerated(string reservationCode, string Reservation_TXT)
        {
            if (!File.Exists(reservationCode))
            {
                return false;
            }

            List<string> existingCode = File.ReadAllLines(Reservation_TXT).ToList();

            return existingCode.Contains(reservationCode);
        }

        public static List<Reservation> GetReservations() 
        {
            List<Reservation> res = new List<Reservation>();
            foreach (string line in File.ReadLines(Reservation_TXT))
            {
                string[] parts = line.Split(",");
                string reservationCode = parts[0];
                string flightCode = parts[1];
                string airline = parts[2];
                double cost = double.Parse(parts[3]);
                string name = parts[4];
                string citizenship = parts[5];
                string status = parts[6];

                Reservation newReservation = new Reservation(reservationCode, flightCode, airline, cost, name, citizenship, status);
                res.Add(newReservation);
            }
            reservations = res;
            return res;
        }

        public void AddReservation(Reservation res)
        {
            File.AppendAllText(Reservation_TXT, $"{res.Code},{res.FlightCode},{res.Airline},{res.Cost},{res.Name},{res.Citizenship},{res.Active}\n");            
        }

        public void UpdateReservation(Reservation res)
        {
            var lines = File.ReadAllLines(Reservation_TXT).ToList();

            // TODO
            // Add code to change the status from Active to Cancelled for the selected flight
            // and update the record in the reservation.csv file  
            // ...................................
            // done by Sneha on 7th March

            // We created a loop that iterated through each line in the list.
            // splits the line using ',' and checks if the reservation code in the current line matches the reservation passed as ('res')
            // if there's match, the code updates the 6th index of array from array to cancelled.
            // join the parts back into a string seperated by comma and updates the line in the list.
            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split(",");
                string reservationCode = parts[0];

                if (reservationCode.Equals(res.Code))
                {
                    // TODO: Update the status from Active to Cancelled
                    parts[6] = "Cancelled";
                }

                lines[i] = string.Join(",", parts);
            }

            File.WriteAllLines(Reservation_TXT, lines);
        }
    }
}
