using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using T__Shop.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Authentication;
using MailKit.Security;

namespace T__Shop.Controllers
{
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SendEmail()
        {
            var emailMessage = new MimeMessage();
            string message = "Thank you for order , wait for our call";
            emailMessage.From.Add(new MailboxAddress("TRIPTATTOO", "university.mvc.test@gmail.com"));
            emailMessage.Subject = "Order";
            emailMessage.To.Add(new MailboxAddress("", User.Identity.Name));
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587,false);
                client.Authenticate("university.mvc.test@gmail.com", "Qwert_123");
                client.Send(emailMessage);

                client.Disconnect(false);
            }
            return RedirectToAction("Index", "Orders");
        }
    }
}
