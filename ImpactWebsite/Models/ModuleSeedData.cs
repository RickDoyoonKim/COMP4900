using ImpactWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactWebsite.Models
{
    public class ModuleSeedData
    {
        public static void Initialize(ApplicationDbContext db)
        {
            getModule(db);
        }

        private static void getModule(ApplicationDbContext db)
        {
            if (!db.Modules.Any())
            {
                db.Modules.Add(new Module
                {
                    ModuleName = "Operational blueprint and asset-level data",
                    ModuleUrl = "~/Images/unique_insight.jpg",
                    DeliveryDays = 3,
                    Description = "Operational blueprint and asset-level data Description",
                });

                db.Modules.Add(new Module
                {
                    ModuleName = "Social Impact metrics",
                    ModuleUrl = "~/Images/our_methodology.jpg",
                    DeliveryDays = 3,
                    Description = "Social Impact metrics Description",
                });

                db.Modules.Add(new Module
                {
                    ModuleName = "Environmental impact metrics",
                    ModuleUrl = "~/Images/sustainability.jpg",
                    DeliveryDays = 3,
                    Description = "Environmental impact metrics Description",
                });
            }
        }
    }
}
