using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using TheWorld.Models.Context;
using TheWorld.Services;
using TheWorld.ViewModels;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private IWorldRepository _repos;
        private ILogger<AppController> _logger;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IWorldRepository repos, ILogger<AppController> logger, IConfigurationRoot config)
        {
            _mailService = mailService;
            _repos = repos;
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                var data = _repos.GetAllTrips();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all trips at Index page: {ex.Message}");
                return Redirect("/error");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            var email = _config["MailSettings:ToAddress"];
            _mailService.SendEmail(email, viewModel.Email, viewModel.Name, viewModel.Message);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
