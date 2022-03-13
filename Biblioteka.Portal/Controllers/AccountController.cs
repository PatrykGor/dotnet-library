using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Biblioteka.Models;
using Biblioteka.Services.Interfaces;

namespace Biblioteka.Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            try
            {
                string token = _userService.Login(username, password);
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("token", token);
                return Redirect("~/Home/Library");
            }
            catch (ArgumentException ex)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Błędny login lub hasło";
                _logger.LogError(ex.Message);
                return Redirect("~/Home/Index");
            }
        }

        [HttpPost]
        public IActionResult Register([FromForm] string username, [FromForm] string password)
        {
            _logger.LogInformation($"Register {nameof(username)}: {username}");
            _logger.LogInformation($"Register {nameof(password)}: {password}");
            _logger.LogDebug(password);
            try
            {
                _userService.Register(username, password);
                TempData["MessageType"] = "success";
                TempData["Message"] = "Utworzono konto";
                return Redirect("~/Home/Index");
            }
            catch (ArgumentException ex)
            {
                TempData["MessageType"] = "danger";
                TempData["Message"] = "Konto o podanej nazwie użytkownika już istnieje";
                _logger.LogError(ex.Message);
                return Redirect("~/Home/Index");
            }
        }
    }
}
