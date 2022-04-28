using System;
using Entities;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
