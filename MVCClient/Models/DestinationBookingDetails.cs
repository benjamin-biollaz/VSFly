using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace MVCClient.Models
{
    public class DestinationBookingDetails
    {
        public int FlightNo { get; set; }
        public float PaidPrice { get; set; }
        public string Destination { get; set; }
        public Person Passenger { get; set; }
    }
}
