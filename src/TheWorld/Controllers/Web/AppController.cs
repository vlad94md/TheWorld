using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheWorld.Models.Context;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private WorldContext _context;


        public AppController(IMailService mailService, WorldContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Trips.ToList();
            return View(data);
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
