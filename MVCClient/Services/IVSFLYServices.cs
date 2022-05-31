using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Models;


namespace MVCClient.Services
{
    public interface IVSFLYServices
    {
       public Task<IEnumerable<FlightM>> GetFlights();
    }
}
