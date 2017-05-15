using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImpactWebsite.Data;
using ImpactWebsite.Models.OrderModels;

namespace ImpactWebsite.Controllers
{
    public class OrderModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderModulesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: OrderModules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Modules.Include(o => o.UnitPrice);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModule = await _context.Modules
                .Include(o => o.UnitPrice)
                .SingleOrDefaultAsync(m => m.ModuleId == id);
            if (orderModule == null)
            {
                return NotFound();
            }

            return View(orderModule);
        }

        // GET: OrderModules/Create
        public IActionResult Create()
        {
            ViewData["UnitPriceId"] = new SelectList(_context.UnitPrices, "UnitPriceId", "UnitPriceId");
            return View();
        }

        // POST: OrderModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,ModuleName,DeliveryDays,Description,LongDescription,UnitPriceId")] OrderModule orderModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModule);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UnitPriceId"] = new SelectList(_context.UnitPrices, "UnitPriceId", "UnitPriceId", orderModule.UnitPriceId);
            return View(orderModule);
        }

        // GET: OrderModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModule = await _context.Modules.SingleOrDefaultAsync(m => m.ModuleId == id);
            if (orderModule == null)
            {
                return NotFound();
            }
            ViewData["UnitPriceId"] = new SelectList(_context.UnitPrices, "UnitPriceId", "UnitPriceId", orderModule.UnitPriceId);
            return View(orderModule);
        }

        // POST: OrderModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleId,ModuleName,DeliveryDays,Description,LongDescription,UnitPriceId")] OrderModule orderModule)
        {
            if (id != orderModule.ModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModuleExists(orderModule.ModuleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["UnitPriceId"] = new SelectList(_context.UnitPrices, "UnitPriceId", "UnitPriceId", orderModule.UnitPriceId);
            return View(orderModule);
        }

        // GET: OrderModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModule = await _context.Modules
                .Include(o => o.UnitPrice)
                .SingleOrDefaultAsync(m => m.ModuleId == id);
            if (orderModule == null)
            {
                return NotFound();
            }

            return View(orderModule);
        }

        // POST: OrderModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderModule = await _context.Modules.SingleOrDefaultAsync(m => m.ModuleId == id);
            _context.Modules.Remove(orderModule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderModuleExists(int id)
        {
            return _context.Modules.Any(e => e.ModuleId == id);
        }
    }
}
