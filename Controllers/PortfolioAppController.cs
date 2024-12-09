using Microsoft.AspNetCore.Mvc;
using portfolioApp.Models;
using System.Text.RegularExpressions;
using portfolioApp.Services;

namespace portfolioApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioAppController : Controller
    {

   
        private readonly EmailService _emailService;
        public PortfolioAppController(  EmailService emailService)
        { 
            _emailService = emailService;
        }
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        [HttpPost("Send_Email")]
        public async Task<IActionResult> SendEmail([FromBody] Send_Email model)
        {
            if (string.IsNullOrEmpty(model.name) || string.IsNullOrEmpty(model.email) ||
                string.IsNullOrEmpty(model.subject) || string.IsNullOrEmpty(model.message))
            {
                return BadRequest("All fields are required.");
            }

            if (!IsValidEmail(model.email))
            {
                return BadRequest("Invalid email address.");
            }

            //var emailMessage = $@"<p>Hello {model.name},</p><p>{model.message}</p><p>Subject: {model.subject}</p>";
             var emailMessage = $@"
                       <p><strong>From Email:</strong> {model.email}</p>
                       <p><strong>Subject:</strong> {model.subject}</p>
                       <p>Hello {model.name},</p>
                       <p>{model.message}</p>";


            try
            {
                await _emailService.SendEmailAsyncportfolio(model.email, model.subject, emailMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to send email. Please try again later.");
            }

            return Ok("Email sent successfully.");
        }
        
    }
}
