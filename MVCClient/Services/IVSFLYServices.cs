﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCClient.Models;


namespace MVCClient.Services
{
    public interface IVSFLYServices
    {
       public Task<IEnumerable<FlightM>> GetFlights();
    }
}
