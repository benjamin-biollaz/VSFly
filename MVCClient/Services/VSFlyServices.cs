using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MVCClient.Models;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;


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

        public async Task<int> BookFlight(BookingDetailsM bd)
        {
            var uri = _baseUri + "Flights/" + bd.FlightNo + ":int";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(bd), Encoding.UTF8, "application/json");
            var x = await _client.PostAsync(uri, httpContent);
            return 0;
        }

        public async Task<float> GetAveragePriceByDestination(string destination)
        {
            var uri = _baseUri + "Flights/Flight/" + destination + ":string/averagePrice";
            var priceString = await _client.GetStringAsync(uri);
            return (float) Convert.ToDouble(priceString);
        }

        public async Task<IEnumerable<DestinationBookingDetails>> GetBookingDetailsByDestination(string destination)
        {
            var uri = _baseUri + "Flights/Flight/" + destination + ":string/Booking/Passenger";
            var responseString = await _client.GetStringAsync(uri);
            var bookingDetails =
                JsonConvert.DeserializeObject<IEnumerable<DestinationBookingDetails>>(responseString);
            return bookingDetails;
        }

        public async Task<FlightM> GetFlight(int id)
        {
            var uri = _baseUri + "Flights/Flight/" + id + ":int";

            var reponseString = await _client.GetStringAsync(uri);
            var flight = JsonConvert.DeserializeObject<FlightM>(reponseString);
            return flight;

        }

        public async Task<IEnumerable<FlightM>> GetFlights()
        {
            var uri = _baseUri + "Flights";
            var reponseString = await _client.GetStringAsync(uri);
            var flightList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(reponseString);
            return flightList;
        }

        public async Task<float> GetFlightSalePrice(int id)
        {

            var uri = _baseUri + "Flights/Flight/" + id + ":int/Price";
            var responseString = await _client.GetStringAsync(uri);
            return (float) Convert.ToDouble(responseString);
        }

        public async Task<float> GetTotalSalePrice(int flightId)
        {
            var uri = _baseUri + "Flights/api/Flights/" + flightId + "/totalSale";
            var responseString = await _client.GetStringAsync(uri);
            return (float) Convert.ToDouble(responseString);
        }
    }
}
