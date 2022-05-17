using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {

            DbContext dbContext = new VSFlyContext();

            var database = dbContext.Database;

            Console.WriteLine("Database: " + database);

            bool test = database.EnsureCreated();

            Console.WriteLine(test);

            if (dbContext.Database.EnsureCreated())
                Console.WriteLine("Database has been created");
            else
            {
                Console.WriteLine("Database already exists");
            }

            using (var context = new VSFlyContext())
            {
                /*
                var pilot = context.Pilots.Where(p =>
                    p.PersonId == 4).FirstOrDefault();

                var copilot = context.Pilots.Where(p =>
                    p.PersonId == 3).FirstOrDefault();

                Flight flight = new Flight()
                {
                    BasePrice = 110,
                    Bookings = null,
                    Capacity = 80,
                    CoPilot = copilot,
                    Date = new DateTime(2022, 11, 20),
                    Departure = "Geneve",
                    Destination = "Orly",
                    FreeSeats = 80,
                    Pilot = pilot
                };

                context.Flights.Add(flight);

                Console.WriteLine(pilot.FirstName);
                */

                /*
                var flightList = context.Flights.Where(f =>
                    f.Pilot.PersonId == 4).ToList();

                var flightListQuery = from f in context.Flights
                    where f.Pilot.PersonId == 4 &&
                          f.BasePrice >= 100
                    select f;

                var flightList2 = flightListQuery.ToList();

                

                foreach (var flight in flightList2)
                {
                    Console.WriteLine("Price: " + flight.BasePrice);
                }

                Console.WriteLine("");

                foreach (var flight in flightList)
                {
                    Console.WriteLine("Price: " + flight.BasePrice);
                }
                */

                /*
                var flight = context.Flights.FirstOrDefault(f => f.Pilot.PersonId == 4);

                var booking = new Booking()
                {
                    Flight = flight,
                    PaidPrice = 100,
                    Passenger = new Passenger()
                    {
                        BirthDate = new DateTime(2002, 10, 10),
                        CustomerSince = new DateTime(2000, 10, 12),
                        Email = "jean@gmail.com",
                        FirstName = "Jean",
                        LastName = "Valjean",
                        Status = "Married"
                    }
                };

                context.Bookings.Add(booking);
                */
                var jeanValjean = context.Passengers.FirstOrDefault(p =>
                    p.LastName == "Valjean");

                var flightJEan = context.Flights.Where(f => f.Bookings.Any(b =>
                    b.Passenger == jeanValjean)).FirstOrDefault();

                Console.WriteLine(flightJEan.BasePrice);

                var booking = context.Bookings.Include(f => f.Flight)
                    .FirstOrDefault(b =>
                    b.Passenger.Equals(jeanValjean));

                var flight = context.Flights.Include(f => f.Pilot).FirstOrDefault(f =>
                    f.Equals(booking.Flight));

                var pilot = context.Pilots.FirstOrDefault(p =>
                    p.Equals(flight.Pilot));

                Console.WriteLine("Pilot first name: {0} ", pilot.FirstName);

                context.SaveChanges();
            }
        }
    }
}
