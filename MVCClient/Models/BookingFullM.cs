using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class BookingFullM
    {
        public int FlightNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Destination { get; set; }
        public string Departure { get; set; }
        public DateTime Date { get; set; }
        public float CurrentPrice { get; set; }

    }
}
