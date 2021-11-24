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

            // I decided it would be easier to compare all combinations if I flattned
            // the data out of lists, that way it would be easier to group
            List<string> bikeStrings = new List<string>();
            foreach (Bikes b in bikes)
            {
                b.bikes.Sort();
                string newString = String.Join(", ", b.bikes);
                bikeStrings.Add(newString);
            };

            List<BikeCombination> bikeCombinations = (List<BikeCombination>)bikeStrings
                .GroupBy(b => b)
                .Select(g => new BikeCombination { Combination = g.Key, FamilyCount = g.Count() })
                .OrderByDescending(o => o.FamilyCount)
                .Take(20)
                .ToList();

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
