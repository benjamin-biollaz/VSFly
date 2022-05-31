using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API.Models
{
    public class FlightM
    {
        public string Destination { get; set; }
        public string Departure { get; set; }
        public int FlightNo { get; set; }
        public DateTime Date { get; set; }
    }
}
