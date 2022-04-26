using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public Flight Flight { get; set; }
        public int FlightId { get; set; }
        public Passenger Passenger { get; set; }
        public int PaidPrice { get; set; }
    }
}
