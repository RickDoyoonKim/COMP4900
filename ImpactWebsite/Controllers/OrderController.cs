using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImpactWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ImpactWebsite.Models;
using ImpactWebsite.Models.OrderModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ImpactWebsite.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ImpactWebsite.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private IHostingEnvironment _environment;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly ILogger _logger;
        private static string _EmailAddress;
        private static string _TotalAmount;

        public OrderController(ApplicationDbContext context, 
                               UserManager<ApplicationUser> UserManager, 
                               IHostingEnvironment environment,
                               SignInManager<ApplicationUser> SignInManager,
                               ILoggerFactory loggerFactory)
        {
            _context = context;
            _UserManager = UserManager;
            _environment = environment;
            _SignInManager = SignInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);

            if (_SignInManager.IsSignedIn(User)) { 
                _EmailAddress = await _UserManager.GetEmailAsync(user);
                ViewData["email"] = _EmailAddress;
            }
            List<OrderList> OrderLists = new List<OrderList>();

            var moduleList = _context.Modules.Include(o => o.UnitPrice);

            foreach (var module in moduleList)
            {
                OrderLists.Add(new OrderList()
                {
                    Modules = module
                });
            }

            return View(OrderLists);
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(IFormCollection collection, string email, string totalPrice, string totalDay)
        {
            ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);
            _TotalAmount = totalPrice;
            ViewData["TotalDay"] = totalDay;
            ViewData["TotalAmount"] = totalPrice;
            
            if (_SignInManager.IsSignedIn(User))
            {
                _EmailAddress = await _UserManager.GetEmailAsync(user);
                ViewData["email"] = _EmailAddress;
            } else
            {
                _EmailAddress = email;
            }
            ViewData["email"] = email;
            int NewOrderNumber = 1;
            if (!_context.OrderHeaders.Any())
            {

                _context.OrderHeaders.Add(new OrderHeader()
                {
                    UserEmail = email,
                    OrderedDate = DateTime.Now,
                    OrderNum = NewOrderNumber,

                });
            }
            else
            {
                NewOrderNumber = _context.OrderHeaders.OrderByDescending(o => o.OrderNum).FirstOrDefault().OrderNum + 1;
                _context.OrderHeaders.Add(new OrderHeader()
                {
                    UserEmail = email,
                    OrderedDate = DateTime.Now,
                    OrderNum = NewOrderNumber,
                });
            }

            await _context.SaveChangesAsync();
            
            var lists = collection["modules"];
            foreach (var list in lists)
            {
                var test = JsonConvert.DeserializeObject<OrderList>(list);
                _context.OrderLines.Add(new OrderLine()
                {
                    ModifiedDate = DateTime.Now,
                    OrderHeaderId = _context.OrderHeaders.FirstOrDefault(o => o.OrderNum == NewOrderNumber).OrderHeaderId,
                    ModuleId = test.Modules.ModuleId,
                });

            }

            await _context.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public IActionResult FileUpdaload()
        {
            ViewData["TotalAmount"] = _TotalAmount;
            return PartialView("_Investment");
        }

        [HttpPost]
        public async Task<IActionResult> FileUpdaload(ICollection<IFormFile> files)
        {
            DateTime dtNow = DateTime.Now;
            string UpdateDate = dtNow.ToString("ddMMyyyy");
            string uploads = Path.Combine(_environment.WebRootPath, "uploads/" + UpdateDate + "/" + _EmailAddress);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return View("NewOrder");
        }

        [HttpGet]
        public IActionResult GoRegisterPartial()
        {
            RegisterViewModel model = new RegisterViewModel();

            return PartialView("_Register", model);
        }
        //
        // GET: /Account/_Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult PartialRegister(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return PartialView("_Register", new RegisterViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartialRegister(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, NewsletterRequired = model.NewsLetterRequired };
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _SignInManager.SignInAsync(user, isPersistent: false);
                    //await _userManager.AddToRoleAsync(user, "Member");
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction("NewOrder");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("NewOrder");
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}