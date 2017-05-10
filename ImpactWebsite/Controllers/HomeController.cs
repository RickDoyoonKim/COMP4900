using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImpactWebsite.Data;
using ImpactWebsite.Models;

namespace ImpactWebsite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //temp data            
            var tempModules = new List<Module>();
            if (!tempModules.Any())
            {
                tempModules.Add(new Module
                {
                    ModuleName = "Operational blueprint and asset-level data",
                    ModuleUrl = "~/Images/unique_insight.jpg",
                    DeliveryDays = 3,
                    Description = "Operational blueprint and asset-level data Description",
                });

                tempModules.Add(new Module
                {
                    ModuleName = "Social Impact metrics",
                    ModuleUrl = "~/Images/our_methodology.jpg",
                    DeliveryDays = 3,
                    Description = "Social Impact metrics Description",
                });

                tempModules.Add(new Module
                {
                    ModuleName = "Environmental impact metrics",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Environmental impact metrics Description",
                });

                tempModules.Add(new Module
                {
                    ModuleName = "Governance and controversies",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Governance and controversies Description",
                });

                tempModules.Add(new Module
                {
                    ModuleName = "Upstream and downstream supplier analysis",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Upstream and downstream supplier analysis Description",
                });

                tempModules.Add(new Module
                {
                    ModuleName = "Regulatory, climate-realted and other risk analysis",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Regulatory, climate-realted and other risk analysis Description",
                });

                tempModules.Add(new Module
                {
                    ModuleName = "Benchmarking and targets",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Benchmarking and targets Description",
                });
            }

            var modules = _db.Modules.ToList();
        

            return View(tempModules);
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
