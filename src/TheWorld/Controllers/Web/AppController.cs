using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AppController(IMailService mailService, IWorldRepository repos, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _repos = repos;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var data = _repos.GetAlTrips();
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

            _mailService.SendEmail("testemail@mail.com", viewModel.Email, viewModel.Name, viewModel.Message);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
