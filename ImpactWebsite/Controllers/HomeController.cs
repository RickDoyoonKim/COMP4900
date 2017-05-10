using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImpactWebsite.Data;
using ImpactWebsite.Models;

//test
namespace ImpactWebsite.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _db;

        public IActionResult Index()
        {
            //temp data
            var modules = new List<Module>();
            if (!modules.Any())
            {
                modules.Add(new Module
                {
                    ModuleName = "Operational blueprint and asset-level data",
                    ModuleUrl = "~/Images/unique_insight.jpg",
                    DeliveryDays = 3,
                    Description = "Operational blueprint and asset-level data Description",
                });

                modules.Add(new Module
                {
                    ModuleName = "Social Impact metrics",
                    ModuleUrl = "~/Images/our_methodology.jpg",
                    DeliveryDays = 3,
                    Description = "Social Impact metrics Description",
                });

                modules.Add(new Module
                {
                    ModuleName = "Environmental impact metrics",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Environmental impact metrics Description",
                });
            }
            return View(modules);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
