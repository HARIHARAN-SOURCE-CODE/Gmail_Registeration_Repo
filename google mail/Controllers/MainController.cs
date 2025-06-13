using google_mail.Models;
using google_mail.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace google_mail.Controllers
{
    public class MainController : Controller
    {
        private readonly EmailService _emailService;
        public MainController(EmailService emailService) 
        {
            _emailService = emailService;
        }
        //httpget

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(Register Reg)
        {
            if (ModelState.IsValid)
            {
                string subject = "Registeration successfully";
                string body = $"HELLO {Reg.Name}, <br/> <br/> THANQ YOU FOR REGISTERATION <br/><br/>with regads,<br/><br/>YOUR TEAM";
                try
                {
                    await _emailService.send(Reg.Email, subject, body);
                    TempData["success"] = "Registeration successfully confirmation sent e-mail";
                   
                }
                catch (System.Exception Message)
                {
                    ViewBag.Error = $"Failed to send mail.Error{Message.Message}";
                }
               
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
