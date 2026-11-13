using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models;
using System.Diagnostics;


namespace ShoesShop.MVC.Controllers
{
	public class HomeController : Controller
	{
		//private readonly IMediator _mediator;
        public HomeController()
        {
            
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
