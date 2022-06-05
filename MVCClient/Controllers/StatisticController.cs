using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using MVCClient.Services;

namespace MVCClient.Controllers
{
    public class StatisticController : Controller
    {

        private readonly ILogger<FlightController> _logger;
        private readonly IVSFLYServices vsFlyServices;

        public StatisticController(ILogger<FlightController> logger, IVSFLYServices vfs)
        {
            _logger = logger;
            vsFlyServices = vfs;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchM searchIn)
        {
            //look for flight statistics
            FlightM flight;
            float totalSalePrice;
            try
            {
                flight = await vsFlyServices.GetFlight(Convert.ToInt32(searchIn.SearchInput));
            }
            catch (FormatException e)
            {
                return View("Search");
            }

            if (flight != null)
            {
                totalSalePrice = await vsFlyServices.GetTotalSalePrice(flight.FlightNo);
                FlightStatM fs = new FlightStatM()
                {
                    Date = flight.Date,
                    Departure = flight.Departure,
                    Destination = flight.Destination,
                    FlightNo = flight.FlightNo,
                    TotalSale = totalSalePrice
                };
                return View("FlightStat", fs);
            }

            //look for destination statistics
           
            return View();
        }


    }
}
