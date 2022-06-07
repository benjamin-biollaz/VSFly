using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvcClient2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MVCWebAPIClient;

namespace mvcClient2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            string baseURI = "https://localhost:44303";

            FlightClient flightClient = new FlightClient(baseURI, client);
            ICollection<MVCWebAPIClient.FlightM> listOfFlights = await flightClient.FlightsAllAsync();
            return View(listOfFlights);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
