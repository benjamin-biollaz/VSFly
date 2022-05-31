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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVSFLYServices vsFlyServices;

        public HomeController(ILogger<HomeController> logger, IVSFLYServices vfs)
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
