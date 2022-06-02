﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Web_API.Models
{
    public class FlightBookingDetailsM
    {
        public int FlightNo { get; set; }
        public float PaidPrice { get; set; }
        public Person Passenger { get; set; }
    }
}
