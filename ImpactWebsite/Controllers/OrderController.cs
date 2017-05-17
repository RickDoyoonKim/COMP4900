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
using Microsoft.Extensions.Options;

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
        private static string _TotalDay;
        private static int _OrderNumber;
        private readonly string _externalCookieScheme;

        public OrderController(ApplicationDbContext context,
                               UserManager<ApplicationUser> UserManager,
                               IHostingEnvironment environment,
                               SignInManager<ApplicationUser> SignInManager,
                               IOptions<IdentityCookieOptions> identityCookieOptions,
                               ILoggerFactory loggerFactory)
        {
            _context = context;
            _UserManager = UserManager;
            _environment = environment;
            _SignInManager = SignInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
        }
        public async Task<IActionResult> Index(string message)
        {
            ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);
            @ViewData["error"] = message;
            if (_SignInManager.IsSignedIn(User))
            {
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NewOrder()
        {
            ViewData["DeliverDate"] = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)).ToString("MMM dd yyyy");
            ViewData["TotalDay"] = _TotalDay;
            ViewData["TotalAmount"] = _TotalAmount;
            ViewData["LoggedinUserId"] = _context.OrderHeaders.FirstOrDefault(o => o.OrderNum == _OrderNumber).UserId;
            ViewData["orderNumber"] = _OrderNumber;

            var OrderLines = _context.OrderLines.Where(o => o.OrderHeader.OrderNum == _OrderNumber).Include(o => o.Module.UnitPrice);
            return View(OrderLines.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(IFormCollection collection, string email, string totalPrice, string totalDay)

        {
            int totalAmount = 0;

            ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);
            ApplicationUser TempUser;
            _TotalAmount = totalPrice;
            _TotalDay = totalDay;
            ViewData["DeliverDate"] = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)).ToString("MMM dd yyyy");
            ViewData["TotalDay"] = totalDay;
            ViewData["TotalAmount"] = totalPrice;
            
            if (_SignInManager.IsSignedIn(User))
            {
                TempUser = user;
                _EmailAddress = await _UserManager.GetEmailAsync(user);
                ViewData["email"] = _EmailAddress;
            }
            else
            {
                var findUser = await _UserManager.FindByEmailAsync(email);
                var notRegisteredUser = await _UserManager.FindByEmailAsync("temp@user.com");
                if (findUser != null)
                {
                    TempUser = findUser;
                } else
                {
                    TempUser = notRegisteredUser;
                }
                _EmailAddress = email;                
            }
            ViewData["email"] = email;
            ViewData["LoggedinUserId"] = TempUser.Id;

            totalAmount = int.Parse(_TotalAmount);

            if (!_context.OrderHeaders.Any())
            {
                _context.OrderHeaders.Add(new OrderHeader()
                {
                    UserEmail = email,
                    OrderedDate = DateTime.Now,
                    DeliveredDate = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)),
                    OrderNum = 1,
                    UserId = TempUser.Id,
                    TotalAmount = totalAmount * 100
                });
            }
            else
            {
                _OrderNumber = _context.OrderHeaders.OrderByDescending(o => o.OrderNum).FirstOrDefault().OrderNum + 1;
                _context.OrderHeaders.Add(new OrderHeader()
                {
                    UserEmail = email,
                    OrderedDate = DateTime.Now,
                    DeliveredDate = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)),
                    OrderNum = _OrderNumber,
                    UserId = TempUser.Id,
                    TotalAmount = totalAmount * 100
                });
            }

            await _context.SaveChangesAsync();

            var lists = collection["modules"];
            foreach (var list in lists)
            {
                var jsonObj = JsonConvert.DeserializeObject<OrderList>(list);
                _context.OrderLines.Add(new OrderLine()
                {
                    ModifiedDate = DateTime.Now,
                    OrderHeaderId = _context.OrderHeaders.FirstOrDefault(o => o.OrderNum == _OrderNumber).OrderHeaderId,
                    ModuleId = jsonObj.Modules.ModuleId,
                    ModuleName = jsonObj.Modules.ModuleName
                });
            }
            await _context.SaveChangesAsync();

            var OrderLines = _context.OrderLines.Where(o => o.OrderHeader.OrderNum == _OrderNumber).Include(o => o.Module.UnitPrice);

            ViewData["orderNumber"] = _OrderNumber;
            return View(OrderLines.ToList());
        }
        [HttpGet]
        public IActionResult FileUpdaload()
        {
            ViewData["email"] = _EmailAddress;
            ViewData["TotalAmount"] = _TotalAmount;
            return PartialView("_Investment");
        }

        [HttpPost]
        public async Task<IActionResult> FileUpdaload(ICollection<IFormFile> files)
        {
            ViewData["TotalAmount"] = _TotalAmount;
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
            var OrderLines = _context.OrderLines.Where(o => o.OrderHeader.OrderNum == _OrderNumber).Include(o => o.Module.UnitPrice);
            ViewData["orderNumber"] = _OrderNumber;
            return View("NewOrderPayOnly", OrderLines.ToList());
        }

        [HttpGet]
        public IActionResult GoRegisterPartial()
        {
            RegisterViewModel model = new RegisterViewModel();

            return PartialView("_Register", model);
        }

        [HttpGet]
        public IActionResult PartialModuleDetail(string id)
        {
            var DetailModules = _context.Modules.FirstOrDefault(m => m.ModuleId == Convert.ToInt32(id));
            return PartialView("_PartialModuleDetail", DetailModules);
        }
        [HttpGet]
        public async Task<IActionResult> RegisterLogin(int id)
        {
            ViewData["orderNumber"] = _OrderNumber;
            string userEmail = _context.OrderHeaders.FirstOrDefault(o => o.OrderNum == id).UserEmail;

            if (await _UserManager.FindByEmailAsync(userEmail) == null)
            {
                ViewData["checkUser"] = "NeedRegister";
            } else
            {
                ViewData["checkUser"] = "NeedLogin";
            }

            return View("RegisterLogin");
        }
        //
        // GET: /Account/_Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult PartialRegister(string returnUrl = null)
        {
            ViewData["email"] = _EmailAddress;
            ViewData["orderNumber"] = _OrderNumber;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartialRegister(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["email"] = _EmailAddress;
            ViewData["orderNumber"] = _OrderNumber;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, NewsletterRequired = model.NewsLetterRequired };
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _SignInManager.SignInAsync(user, isPersistent: false);
                    _context.OrderHeaders.FirstOrDefault(o => o.OrderNum == _OrderNumber).UserId = user.Id;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation(3, "User created a new account with password.");
                    await _UserManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("NewOrder");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PartialLogin(string returnUrl = null)
        {
            ViewData["email"] = _EmailAddress;
            ViewData["orderNumber"] = _OrderNumber;
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartialLogin(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["email"] = _EmailAddress;
            ViewData["orderNumber"] = _OrderNumber;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToAction("NewOrder");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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