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

            var OrderLines = _context.OrderLines.Where(o => o.OrderHeader.OrderNum == _OrderNumber).Include(o => o.Module.UnitPrice);
            return View(OrderLines.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(IFormCollection collection, string email, string totalPrice, string totalPriceInt, string totalDay)

        {
            int NewOrderNumber = 1;

            ApplicationUser user = await _UserManager.GetUserAsync(HttpContext.User);
            ApplicationUser TempUser;
            _TotalAmount = totalPrice;
            _TotalDay = totalDay;
            ViewData["DeliverDate"] = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)).ToString("MMM dd yyyy");
            ViewData["TotalDay"] = totalDay;
            ViewData["TotalAmount"] = totalPrice;
            
            int totalAmount = Int32.Parse(totalPriceInt);

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

            if (!_context.OrderHeaders.Any())
            {
                _context.OrderHeaders.Add(new OrderHeader()
                {
                    UserEmail = email,
                    OrderedDate = DateTime.Now,
                    DeliveredDate = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)),
                    OrderNum = NewOrderNumber,
                    UserId = TempUser.Id,
                    TotalAmount = totalAmount
                });
            }
            else
            {
                NewOrderNumber = _context.OrderHeaders.OrderByDescending(o => o.OrderNum).FirstOrDefault().OrderNum + 1;
                _context.OrderHeaders.Add(new OrderHeader()
                {
                    UserEmail = email,
                    OrderedDate = DateTime.Now,
                    DeliveredDate = DateTime.Now.AddDays(Convert.ToDouble(_TotalDay)),
                    OrderNum = NewOrderNumber,
                    UserId = TempUser.Id,
                    TotalAmount = totalAmount
                });
            }

            await _context.SaveChangesAsync();

            try
            {
                var newOrderHeader = _context.OrderHeaders.SingleOrDefault(x => x.OrderNum == NewOrderNumber);
                ViewData["OrderId"] = newOrderHeader.OrderHeaderId;
                if(ViewData["OrderId"] == null)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException source: {0}", e.Source);
            }

            var lists = collection["modules"];
            foreach (var list in lists)
            {
                var jsonObj = JsonConvert.DeserializeObject<OrderList>(list);
                _context.OrderLines.Add(new OrderLine()
                {
                    ModifiedDate = DateTime.Now,
                    OrderHeaderId = _context.OrderHeaders.FirstOrDefault(o => o.OrderNum == NewOrderNumber).OrderHeaderId,
                    ModuleId = jsonObj.Modules.ModuleId,
                    ModuleName = jsonObj.Modules.ModuleName
                });
            }
            await _context.SaveChangesAsync();

            //var OrderHeaderModel = _context.OrderHeaders
            //                      .Include(o => o.OrderLines)
            //                      .FirstOrDefault(o => o.OrderNum == NewOrderNumber);
            _OrderNumber = NewOrderNumber;
            var OrderLines = _context.OrderLines.Where(o => o.OrderHeader.OrderNum == NewOrderNumber).Include(o => o.Module.UnitPrice);
            ViewData["orderNumber"] = NewOrderNumber;
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
        [AllowAnonymous]
        public IActionResult PartialModuleDetail(string id)
        {
            var DetailModules = _context.Modules.FirstOrDefault(m => m.ModuleId == Convert.ToInt32(id));
            return PartialView("_PartialModuleDetail", DetailModules);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterLogin(int id)
        {
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
                var user = new ApplicationUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, NewsletterRequired = model.NewsLetterRequired };
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

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PartialLogin(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return PartialView("_Login", new LoginViewModel());
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartialLogin(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
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
                    //return RedirectToAction("NewOrder");
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(model);
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