using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IAppUnitOfWork uow,ILogger<HomeController> logger)
        {
            
            _logger = logger;
            _uow = uow;
        }

        public class Vm
        {
            public string Foo { get; set; } = default!;
            public string Bar { get; set; } = default!;
            [BindRequired] public int Temperature { get; set; } = 489;
        }
        
        // public IActionResult Test()
        // {
        //     var viewmodel = 5;
        //     return View(viewmodel);
        // }


        public async Task<IActionResult> Index()
        {
            // var res = await _uow.Teams.GetAllAsync(User.GetUserId()!.Value);
            // await _uow.SaveChangesAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
                );
            return LocalRedirect(returnUrl);
        }
    }
}