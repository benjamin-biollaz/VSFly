using System;
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

                Pilot pilot = new Pilot()
                {
                    BirthDate = DateTime.Now,
                    Email = "biollaz@gmail.com",
                    FirstName = "Jean-Claude",
                    FlightHours = 4,
                    FlightSchool = "Harvard",
                    HireDate = DateTime.Now,
                    LastName = "Dusse",
                    LicenseDate = DateTime.Now,
                    PassportNumber = "CH12ec4bj3dddfes",
                };

                context.Pilots.Add(pilot);
                context.SaveChanges();
            }
        }
    }
}
