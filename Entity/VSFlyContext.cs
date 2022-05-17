using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entities
{
    public partial class VSFlyContext : DbContext
    {
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }

        public static readonly ILoggerFactory myLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public static string ConnectionString { get; set; } = @"Data Source=153.109.124.35;Initial Catalog=Biollaz_Gaillard_VSFly;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                optionsBuilder.UseLoggerFactory(myLoggerFactory).EnableSensitiveDataLogging();
            }
        }
    }
}
