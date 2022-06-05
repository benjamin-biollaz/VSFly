using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class DestinationStatFull
    {
        public IEnumerable<DestinationBookingDetails> DestinationBookingDetails { get; set; }
        public float AverageSalePrice { get; set; }

    }
}
