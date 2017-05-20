using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using ImpactWebsite.Models.BillingModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using ImpactWebsite.Common;
using ImpactWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using ImpactWebsite.Data;
using ImpactWebsite.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace ImpactWebsite.Controllers
{
    [Authorize]
    public class BillingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private static int _amountInt;
        private static string _emailAddress;

        public BillingController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
   
        public async Task<IActionResult> Index(string id, int orderId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<BillingDetailViewModel> billingVM = new List<BillingDetailViewModel>();
            var totalAmount = 0;
            var moduleCount = 0;            

            if (_signInManager.IsSignedIn(User))
            {
                _emailAddress = await _userManager.GetEmailAsync(user);
                ViewData["email"] = _emailAddress;
            }

            if (id != null)
            {
                var billingDetails = (from u in _context.Users
                                      join oh in _context.OrderHeaders on u.Id equals oh.UserId
                                      join ol in _context.OrderLines on oh.OrderHeaderId equals ol.OrderHeaderId
                                      join m in _context.Modules on ol.ModuleId equals m.ModuleId
                                      select new
                                      {
                                          OrderNumber = oh.OrderNum,
                                          UserId = u.Id,
                                          UserEmail = u.Email,
                                          ModuleId = m.ModuleId,
                                          ModuleName = m.ModuleName,
                                          UnitPrice = m.UnitPrice.Price,
                                          TotalAmount = oh.TotalAmount,
                                          OrderStatus = oh.OrderStatus,
                                      }).ToList();

                var temps = billingDetails.Where(x => x.UserId == id).Where(y => y.OrderNumber == orderId).ToList();

                foreach (var billing in temps)
                {
                    billingVM.Add(new BillingDetailViewModel()
                    {
                        OrderHeaderId = billing.OrderNumber,
                        UserId = billing.UserId,
                        UserEmail = billing.UserEmail,
                        ModuleId = billing.ModuleId,
                        ModuleName = billing.ModuleName,
                        UnitPrice = billing.UnitPrice,
                        TotalAmount = billing.TotalAmount,
                        OrderStatus = billing.OrderStatus
                    });
                };

                foreach (var billing in billingVM)
                {
                    moduleCount += 1;
                    totalAmount = billing.TotalAmount;
                }

                ViewBag.PaymentDetails = billingVM;

                ViewData["amount"] = totalAmount;
                ViewData["amountDisplay"] = totalAmount / 100;
                ViewData["moduleCount"] = moduleCount;
                ViewData["orderId"] = orderId;
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var billingAddress = await _context.BillingAddresses.LastOrDefaultAsync(x => x.UserId == userId);

            ViewData["billingAddressId"] = (billingAddress != null) ? (int)billingAddress.BillingAddressId : -1;
            ViewBag.BillingAddress = billingAddress;
            _amountInt = totalAmount;

            return View(billingVM);
        }

        public async Task<IActionResult> CompleteDefaultOrder(int orderId)
        {
            // Check orderNum
            var completedOrders = _context.OrderHeaders.Where(x => x.OrderNum == orderId);

            foreach (var order in completedOrders)
            {
                order.OrderStatus = OrderStatusList.Completed;
            }

            await _context.SaveChangesAsync();

            return View(completedOrders);
        }

        public async Task<IActionResult> Charge(
            string stripeEmail
            ,string stripeToken
            ,int orderId
            ,int bAddressId
            )
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            // check OrderNumber
            var completedOrders = _context.OrderHeaders.Where(x => x.OrderNum == orderId);

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken,                
            });

            var billingAddress = await _context.BillingAddresses.LastOrDefaultAsync(x=>x.BillingAddressId == bAddressId);

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = _amountInt,
                Description = "Module Charge",
                Currency = "cad",
                CustomerId = customer.Id,
            });

            StripeAddress stripeAddress = new StripeAddress()
            {
                Line1 = billingAddress.AddressLine1,
                Line2 = billingAddress.AddressLine2,
                CityOrTown = billingAddress.City,
                State = billingAddress.State,
                PostalCode = billingAddress.ZipCode,
                Country = billingAddress.Country
            };

            foreach (var order in completedOrders) { order.OrderStatus = OrderStatusList.Completed; }

            await _context.SaveChangesAsync();

            return View(completedOrders);
        }

        public async Task<IActionResult> PaymentHistory()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            return View(await _context.OrderHeaders.Where(m => m.UserId == userId).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderlines = await _context.OrderLines.Where(m => m.OrderHeaderId == id).ToListAsync();
    
            if (orderlines == null)
            {
                return NotFound();
            }

            return View(orderlines);
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> BillingAddress()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var billingAddress = await _context.BillingAddresses.LastOrDefaultAsync(w => w.UserId == userId);

            return View(billingAddress);
        }

        [HttpPost]
        public async Task<IActionResult> BillingAddress(BillingAddress billingAddress)
        {
            if (ModelState.IsValid)
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                var currentBillingAddresses = _context.BillingAddresses
                    .Where(x => x.UserId == userId).ToList();

                if (currentBillingAddresses.Any())
                {
                    foreach (var address in currentBillingAddresses)
                    {
                        _context.BillingAddresses.Remove(address);
                    }

                   await _context.SaveChangesAsync();
                }

                billingAddress.UserId = userId;
                user.BillingAddress = billingAddress;                
                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index");
            }

            return View(billingAddress);
        }

    }
}