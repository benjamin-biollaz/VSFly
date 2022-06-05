using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MVCClient.Models;


namespace MVCClient.Services
{
    public class VSFlyServices : IVSFLYServices
    {
        private readonly HttpClient _client;
        private readonly string _baseUri;

        public VSFlyServices(HttpClient httpClient)
        {
            _client = httpClient;
            _baseUri = "https://localhost:44303/api/";
        }

        public Task<int> BookFlight(int id, string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public Task<float> GetAveragePriceByDestination(string destination)
        {
            throw new NotImplementedException();
        }

        public Task<FlightM> GetFlight(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FlightM>> GetFlights()
        {
            var uri = _baseUri + "Flights";

            var reponseString = await _client.GetStringAsync(uri);
            var flightList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(reponseString);
            return flightList;
        }

        public Task<float> GetFlightSalePrice()
        {
            throw new NotImplementedException();
        }

        public Task<float> GetTotalSalePrice()
        {
            throw new NotImplementedException();
        }
    }
}
