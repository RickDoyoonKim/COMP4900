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

namespace ImpactWebsite.Controllers
{
    public class BillingController : Controller
    {
        private StripeSettings _stripeSettings;
        private UserManager<ApplicationUser> _userManager;

        /*
        public ChargeController(
            StripeSettings stripeSettings,
            UserManager<ApplicationUser> userManager)
        {
            _stripeSettings = stripeSettings;
            _userManager = userManager;
        }*/

        public async Task<IActionResult> Index()
        {
            /*
            var userId = User.GetUserId();
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            ViewBag.StripeKey = _stripeSettings.PublishableKey;*/

            return View();
        }

        public async Task<IActionResult> Charge([FromForm]string stripeToken)
        {
            var userId = User.GetUserId();
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (string.IsNullOrEmpty(user.Id))
            {
                var customers = new StripeCustomerService();
                var charges = new StripeChargeService();

                var customer = customers.Create(new StripeCustomerCreateOptions
                {
                    Email = $"{user.Email}",
                    Description = $"{user.Email}[{userId}]",
                    SourceToken = stripeToken
                });

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = 500,
                    Description = "Sample Charge",
                    Currency = "usd",
                    CustomerId = customer.Id
                });

            }



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
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}