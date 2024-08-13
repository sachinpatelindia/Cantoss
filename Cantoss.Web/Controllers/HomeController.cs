using Cantoss.Service.Portals;
using Cantoss.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cantoss.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPortalService _portalService;
        public HomeController(ILogger<HomeController> logger, IPortalService portalService)
        {
            _portalService = portalService;
             _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            var contact =await _portalService.GetPortalById();
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
