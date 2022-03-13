using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteka.Portal.Models;
using Biblioteka.Services.Interfaces;
using Biblioteka.Models.Queries;
using Microsoft.AspNetCore.Http;
using System.Text;
using Biblioteka.Models;

namespace Biblioteka.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        private bool validToken()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null)
                return false;
            string validToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}{DateTime.Today}"));
            string token = HttpContext.Session.GetString("token");
            _logger.LogInformation($"validating token: {token} against validtoken: {validToken}");
            return token == validToken;
        }

        private IActionResult endSession(string type, string message)
        {
            TempData["MessageType"] = type;
            TempData["Message"] = message;
            return Redirect("~/Home/Index");
        }

        public HomeController(ILogger<HomeController> logger,
            IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Library()
        {
            if (!validToken())
            {
                return endSession("danger", "Sesja wygasła");
            }
            ViewBag.Books = _bookService.GetAllBooks();
            return View();
        }
        [HttpPost]
        public IActionResult Library([FromForm] BookQuery searchQuery)
        {
            if (!validToken())
            {
                return endSession("danger", "Sesja wygasła");
            }
            ViewBag.Books = _bookService.GetBooks(searchQuery);
            return View();
        }

        public IActionResult Book([FromQuery] bool search, int? id)
        {
            if (!validToken())
            {
                return endSession("danger", "Sesja wygasła");
            }
            if (search)
            {
                ViewData["ActionText"] = "Szukaj";
                ViewData["Action"] = "Library";
                ViewData["Controller"] = "Home";
            }
            if (id != null)
            {
                ViewBag.Book = _bookService.GetBook((int)id);
                ViewData["ActionText"] = "Zmień";
                ViewData["Action"] = "Edit";
                ViewData["Controller"] = "Books";
            }
            if (!search && id == null)
            {
                ViewData["ActionText"] = "Dodaj";
                ViewData["Action"] = "Add";
                ViewData["Controller"] = "Books";
            }
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
