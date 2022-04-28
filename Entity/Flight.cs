using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        [InverseProperty("BookingId")]
        public virtual List<Booking> Bookings { get; set; }
        public DateTime Date { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public int FreeSeats { get; set; }
        public Pilot Pilot { get; set; }
        public Pilot CoPilot { get; set; }
        public int BasePrice { get; set; }
        public int Capacity { get; set; }

    }
}
