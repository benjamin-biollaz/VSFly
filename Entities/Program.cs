using System;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext dbContext = new VSFlyContext();

            var e = dbContext.Database.EnsureCreated();

            if (e)
                Console.WriteLine("Database has been created");
            else
            {
                Console.WriteLine("Database already exists");
            }
        }
    }
}
