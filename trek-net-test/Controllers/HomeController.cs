using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trek_net_test.Models;
using trek_net_test.Services;

namespace trek_net_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrekApiService _trekApiService;

        public HomeController(ILogger<HomeController> logger, ITrekApiService trekApiService)
        {
            _logger = logger;
            _trekApiService = trekApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Bikes> bikes = new List<Bikes>();

            bikes = await _trekApiService.GetBikes();


            // Got stuck trying to set up group of new bike combinations. I think the issue is with my select statement,
            // I would want to do some testing to make sure the GroupBy is actually grouping the bikes by distinct lists as well
            // Given more time, my path forward would be to break up this query a bit to ensure I am getting the values that I'm
            // expecting and then find what is causing the issue when trying to bring it all together.
            List<BikeCombination> bikeCombinations = (List<BikeCombination>)bikes
                .GroupBy(b => b.bikes)
                .Select(g => new BikeCombination { Combination = g.ToString(), FamilyCount = g.Count() })
                .ToList()
                .OrderByDescending(o => o.FamilyCount)
                .Take(21);

            return View(bikeCombinations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
