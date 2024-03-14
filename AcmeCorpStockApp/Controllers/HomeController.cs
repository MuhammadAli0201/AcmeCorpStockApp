using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.CustomElements;
using AcmeCorpStockApp.Dtos;
using AcmeCorpStockApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcmeCorpStockApp.Controllers
{
    [Route("Home")]
    [CustomAuthAttribute]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IManageStockAppUserService _manageStockAppUserService;

        public HomeController(ILogger<HomeController> logger,
            IManageStockAppUserService manageStockAppUserService)
        {
            _logger = logger;
            _manageStockAppUserService = manageStockAppUserService;
        }
        [Route("")]
        [Route("/")]
        [Route("acme-project.com")]
        public IActionResult Index()
        {
            StockAppUser user = _manageStockAppUserService.GetCurrentUserFromCookie();
            HomeIndexDTO homeIndexDTO = new HomeIndexDTO { Email = user.Email, Type = user.Type };
            return View(homeIndexDTO);
        }

        [HttpGet]
        [Route("RegistrarOnly")]
        public IActionResult RegistrarOnly()
        {
            StockAppUser user = _manageStockAppUserService.GetCurrentUserFromCookie();

            if (user.Type == "Registrar")
            {
                return View();
            }
            else
            {
                ModelState.AddModelError("", "You are not the registraar");

                HomeIndexDTO homeIndexDTO = new HomeIndexDTO { Email = user.Email, Type = user.Type };
                return View("Index", homeIndexDTO);
            }
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            bool isLogout = _manageStockAppUserService.LogOut();

            if (isLogout)
            {
                return RedirectToAction("login", "account");
            }
            else
            {
                return View("index");
            }
        }
    }
}
