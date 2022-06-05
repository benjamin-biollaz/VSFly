using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class StatisticController : Controller
    {
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(SearchM searchIn)
        {

           
            return View();
        }
    }
}
