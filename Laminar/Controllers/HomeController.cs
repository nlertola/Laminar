using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Laminar.Models;
using Microsoft.AspNetCore.Authorization;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace Laminar.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            SendNewUserConfirmation().Wait();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        static async System.Threading.Tasks.Task SendNewUserConfirmation()
        {
            var apiKey = Environment.GetEnvironmentVariable("laminar03071998");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply@laminar.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("nlertola@live.com", "Nick User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
