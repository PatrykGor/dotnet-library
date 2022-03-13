using Biblioteka.Models;
using Biblioteka.Models.Queries;
using Biblioteka.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Biblioteka.Portal.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
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

        public BooksController(ILogger<BooksController> logger,
            IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult Add([FromForm] BookQuery book)
        {
            if (!validToken())
                return endSession("danger", "sesja wygasła");
            _bookService.AddBook(book);
            TempData["MessageType"] = "success";
            TempData["Message"] = "Pomyślnie dodano książkę";
            return Redirect($"~/Home/Library");
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Book book)
        {
            if (!validToken())
                return endSession("danger", "sesja wygasła");
            _bookService.EditBook(book);
            TempData["MessageType"] = "success";
            TempData["Message"] = "Pomyślnie zaktualizowano książkę";
            return Redirect($"~/Home/Library");
        }

        public IActionResult Remove(int id)
        {
            if (!validToken())
                return endSession("danger", "sesja wygasła");
            try
            {
                _bookService.DeleteBook(id);
            }
            catch (ArgumentException ex)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Błąd serwera: podana książka nie istnieje.";
                return Redirect("~/Home/Library");
            }
            TempData["MessageType"] = "warning";
            TempData["Message"] = $"Usunięto książkę o Id: {id}";
            return Redirect("~/Home/Library");
        }
    }
}
