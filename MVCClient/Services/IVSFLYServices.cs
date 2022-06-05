using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCClient.Models;


namespace MVCClient.Services
{
    public interface IVSFLYServices
    {
       public Task<IEnumerable<FlightM>> GetFlights();
       public Task<float> GetFlightSalePrice();
       public Task<float> GetTotalSalePrice();
       public Task<int> BookFlight(BookingDetailsM bd);
       public Task<FlightM> GetFlight(int id);
       public Task<float> GetAveragePriceByDestination(string destination);

    }
}
