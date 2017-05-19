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
    public class PromotionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static string _prePromoCode;

        public PromotionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Promotions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promotions.ToListAsync());
        }

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .SingleOrDefaultAsync(m => m.PromotionId == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // GET: Promotions/Create
        public IActionResult Create()
        {
            var promotion = new Promotion();
            promotion.PromotionCode = GetPromoCode();
            promotion.DateFrom = new DateTime(2017,01,01);
            promotion.DateTo = new DateTime(2017, 01, 01);
            return View(promotion);
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromotionId,PromotionName,PromotionCode,DiscountRate,DateFrom,DateTo,Description,IsActive,ModifiedDate")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                promotion.ModifiedDate = DateTime.Now;
                _context.Add(promotion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions.SingleOrDefaultAsync(m => m.PromotionId == id);
            _prePromoCode = promotion.PromotionCode;
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromotionId,PromotionName,PromotionCode,DiscountRate,DateFrom,DateTo,Description,IsActive,ModifiedDate")] Promotion promotion)
        {
            if (id != promotion.PromotionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_prePromoCode.Equals(promotion.PromotionCode))
                    {
                        _context.Update(promotion);
                        await _context.SaveChangesAsync();
                    }  else {

                        if (_context.Promotions.Any(p => p.PromotionCode.Equals(promotion.PromotionCode)))
                        {
                            ModelState.AddModelError("PromotionCode", "Promotion Code " + promotion.PromotionCode + " is already in use.");
                            return View(promotion);
                        } else { 
                            _context.Update(promotion);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.PromotionId))
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
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions
                .SingleOrDefaultAsync(m => m.PromotionId == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotion = await _context.Promotions.SingleOrDefaultAsync(m => m.PromotionId == id);
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotions.Any(e => e.PromotionId == id);
        }

        public string GetPromoCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[8];
            var random = new Random();
            var finalString = "DEFAULT";

            while (!IsPromoCodeUnique(finalString) || finalString.Equals("DEFAULT")) { 
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                finalString = new String(stringChars);
            }
            return finalString;
        }
        private bool IsPromoCodeUnique(string promoCode)
        {
            if(_context.Promotions.Any(p => p.PromotionCode.Equals(promoCode)))
            {
                return false;
            }
            return true;
        }

    }
}
