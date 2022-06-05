using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MVCClient.Services;

namespace MVCClient.Controllers
{
    public class FlightController : Controller
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IVSFLYServices vsFlyServices;

        public FlightController(ILogger<FlightController> logger, IVSFLYServices vfs)
        {
            _logger = logger;
            vsFlyServices = vfs;
        }

        public async Task<IActionResult> Index()
        {
            var listFlights = await vsFlyServices.GetFlights();

            return View(listFlights);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        public async Task<IActionResult> Book(int id)
        {
            var flight = await vsFlyServices.GetFlight(id);
            BookingFullM bd = new BookingFullM()
            {
                CurrentPrice = flight.CurrentPrice,
                Date = flight.Date,
                FlightNo = flight.FlightNo,
                Departure = flight.Departure,
                Destination = flight.Destination
            };
            return View(bd);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookingFullM full)
        {
            BookingDetailsM bd = new BookingDetailsM()
            {
                FirstName = full.FirstName,
                LastName = full.LastName,
                FlightNo = full.FlightNo
            };
            await vsFlyServices.BookFlight(bd);

            return View("Confirmation", full);
        }

        public IActionResult Confirmation(BookingFullM bdm)
        {
            return View(bdm);
        }

        public IActionResult Average(int id)
        {
          // var averagePrice = await vsFlyServices
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
