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
            var totalAmount = 0;

            if (_signInManager.IsSignedIn(User))
            {
                _emailAddress = await _userManager.GetEmailAsync(user);
                ViewData["email"] = _emailAddress;
            }

            if (id != null)
            {            
                List<BillingDetailViewModel> billingVM = new List<BillingDetailViewModel>();

                var billingDetails = (from u in _context.Users
                                      join oh in _context.OrderHeaders on u.Id equals oh.UserId
                                      join ol in _context.OrderLines on oh.OrderHeaderId equals ol.OrderHeaderId
                                      join m in _context.Modules on ol.ModuleId equals m.ModuleId
                                      select new
                                      {
                                          OrderHeaderId = oh.OrderHeaderId,
                                          UserId = u.Id,
                                          UserEmail = u.Email,
                                          ModuleId = m.ModuleId,
                                          ModuleName = m.ModuleName,
                                          UnitPrice = m.UnitPrice.Price,
                                          TotalAmount = oh.TotalAmount
                                      }).ToList();

                var temps = billingDetails.Where(x => x.UserId == id).Where(y => y.OrderHeaderId == orderId).ToList();

                foreach (var billing in temps)
                {
                    billingVM.Add(new BillingDetailViewModel()
                    {
                        OrderHeaderId = billing.OrderHeaderId,
                        UserId = billing.UserId,
                        UserEmail = billing.UserEmail,
                        ModuleId = billing.ModuleId,
                        ModuleName = billing.ModuleName,
                        UnitPrice = billing.UnitPrice,
                        TotalAmount = billing.TotalAmount
                    });
                };

                ViewBag.PaymentDetails = billingVM;

                var moduleCount = 0;


                foreach (var billing in billingVM)
                {
                    moduleCount += 1;
                    totalAmount = billing.TotalAmount;
                }

                ViewData["amount"] = totalAmount;
                ViewData["amountDisplay"] = totalAmount/100;
                ViewData["moduleCount"] = moduleCount;        
            }

            _amountInt = totalAmount;
            return View();
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

           var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = _amountInt,
                Description = "Module Charge",
                Currency = "cad",
                CustomerId = customer.Id
            });

            return View();
        }

        public IActionResult PaymentHistory()
        {
            List<BillingDetailViewModel> billingVM = new List<BillingDetailViewModel>();

            var billingDetails = (from u in _context.Users
                                  join oh in _context.OrderHeaders on u.Id equals oh.UserId
                                  join ol in _context.OrderLines on oh.OrderHeaderId equals ol.OrderHeaderId
                                  join m in _context.Modules on ol.ModuleId equals m.ModuleId
                                  select new
                                  {
                                      OrderHeaderId = oh.OrderHeaderId,
                                      UserId = u.Id,
                                      UserEmail = u.Email,
                                      ModuleId = m.ModuleId,
                                      ModuleName = m.ModuleName,
                                      UnitPrice = m.UnitPrice.Price,
                                      TotalAmount = oh.TotalAmount
                                  }).ToList();

            foreach (var billing in billingDetails)
            {
                billingVM.Add(new BillingDetailViewModel()
                {
                    OrderHeaderId = billing.OrderHeaderId,
                    UserId = billing.UserId,
                    UserEmail = billing.UserEmail,
                    ModuleId = billing.ModuleId,
                    ModuleName = billing.ModuleName,
                    UnitPrice = billing.UnitPrice,
                    TotalAmount = billing.TotalAmount
                });
            };

            return View(billingVM);
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}