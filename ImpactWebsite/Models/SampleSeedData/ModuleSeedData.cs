using ImpactWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class ModuleSeedData
    {
        public static async Task InitializeModuleSeedAsync(ApplicationDbContext context)
        {
           if (context.Modules.Any())
           {
               return;
           }

           await InsertTestModuleData(context);            
        }

        private static async Task InsertTestModuleData(ApplicationDbContext context)
        {
            context.Modules.Add(new Module
            {
                ModuleName = "Operational blueprint and asset-level data",
                ModuleUrl = "~/Images/unique_insight.jpg",
                DeliveryDays = 3,
                Description = "Operational blueprint and asset-level data Description",
            });

            context.Modules.Add(new Module
            {
                ModuleName = "Social Impact metrics",
                ModuleUrl = "~/Images/our_methodology.jpg",
                DeliveryDays = 3,
                Description = "Social Impact metrics Description",
            });

            context.Modules.Add(new Module
            {
                ModuleName = "Environmental impact metrics",
                ModuleUrl = "~/Images/sustainability.jpg",
                DeliveryDays = 3,
                Description = "Environmental impact metrics Description",
            });

            context.Modules.Add(new Module
            {
                ModuleName = "Governance and controversies",
                ModuleUrl = "~/Images/default_module.jpg",
                DeliveryDays = 3,
                Description = "Governance and controversies Description",
            });

            context.Modules.Add(new Module
            {
                ModuleName = "Upstream and downstream supplier analysis",
                ModuleUrl = "~/Images/default_module.jpg",
                DeliveryDays = 3,
                Description = "Upstream and downstream supplier analysis Description",
            });

            context.Modules.Add(new Module
            {
                ModuleName = "Regulatory, climate-realted and other risk analysis",
                ModuleUrl = "~/Images/default_module.jpg",
                DeliveryDays = 3,
                Description = "Regulatory, climate-realted and other risk analysis Description",
            });

            context.Modules.Add(new Module
            {
                ModuleName = "Benchmarking and targets",
                ModuleUrl = "~/Images/default_module.jpg",
                DeliveryDays = 3,
                Description = "Benchmarking and targets Description",
            });

            await context.SaveChangesAsync();
        }
    }
}
