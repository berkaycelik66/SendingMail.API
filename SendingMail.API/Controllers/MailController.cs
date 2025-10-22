using Microsoft.AspNetCore.Mvc;
using SendingMail.API.Models;
using System.Net;
using System.Net.Mail;

namespace SendingMail.API.Controllers
{
    [ApiController]
    public class MailController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public MailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("/api/send-single-mail")]
        public async Task<IActionResult> SendMail([FromBody] SingleMailRequest request)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var smtpClient = new SmtpClient(smtpSettings["Host"])
                {
                    Port = int.Parse(smtpSettings["Port"]!),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                    EnableSsl = true
                };

                // HTML Ýçeriðini Dinamik Olarak Oluþturma
                string emailBody = GetHtmlBody(request.Subject, request.Message);
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["FromEmail"]!, smtpSettings["BusinessName"]),
                    Subject = request.Subject,
                    Body = emailBody,
                    IsBodyHtml = true
                };

                //Alýcýyý ekle
                mailMessage.To.Add(request.Mail);

                //E-posta gönderme
                await smtpClient.SendMailAsync(mailMessage);
                return Ok(new { Message = "Mail sent to the " + request.Mail });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }

        [HttpPost("/api/send-bulk-mail")]
        public async Task<IActionResult> SendBulkEmail([FromBody] BulkMailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var smtpClient = new SmtpClient(smtpSettings["Host"])
                {
                    Port = int.Parse(smtpSettings["Port"]!),
                    Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                    EnableSsl = true
                };

                // HTML Ýçeriðini Dinamik Olarak Oluþturma
                string emailBody = GetHtmlBody(request.Subject, request.Message);
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings["FromEmail"]!, smtpSettings["BusinessName"]),
                    Subject = request.Subject,
                    Body = emailBody,
                    IsBodyHtml = true
                };

                // Alýcýlarý ekle
                foreach (var email in request.Mails)
                {
                    mailMessage.To.Add(email);
                }

                // E-posta gönderme
                await smtpClient.SendMailAsync(mailMessage);
                return Ok(new
                {
                    Message = $"{request.Mails.Count} mail sent succesfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }

        [NonAction] 
        public string GetHtmlBody(string subject, string message) 
        { 
            var mailBody = $@" 
                    <!DOCTYPE html> 
                    <html>
                      <head>
                        <meta charset='UTF-8' />
                        <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                        <meta http-equiv='X-UA-Compatible' content='ie=edge' />

                        <link href='https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600&display=swap'
                          rel='stylesheet'/>
                      </head>
                      <body
                        style='
                          margin: 0;
                          font-family: 'Poppins', sans-serif;
                          background: #ffffff;
                          font-size: 14px;'>
                        <div
                          style='
                            max-width: 680px;
                            margin: 0 auto;
                            padding: 45px 30px 60px;
                            background: #f4f7ff;
                            background-image: url(https://img.freepik.com/premium-photo/blue-gradient-background_1301176-261.jpg);
                            background-repeat: no-repeat;
                            background-size: 800px 452px;
                            background-position: top center;
                            font-size: 14px;
                            color: #434343;'>
                          <main>
                            <div
                              style='
                                margin: 0;
                                margin-top: 70px;
                                padding: 92px 30px 115px;
                                background-image: url(https://img.freepik.com/premium-photo/blue-gradient-background_1301176-261.jpg);
                                background: #ffffff;
                                border-radius: 30px;'>
                              <div style='width: 100%; max-width: 489px; margin: 0 auto;'>
                                <h1
                                  style='
                                    margin: 0;
                                    font-size: 24px;
                                    font-weight: 500;
                                    color: #1f1f1f;
                                    text-align: center;'>
                                  {subject}
                                </h1>
                                <p
                                  style='
                                    margin: 0;
                                    margin-top: 50px;
                                    font-weight: 500;
                                    letter-spacing: 0.56px;'>
                                  {message}
                                </p>
                              </div>
                            </div>
                          </main>
                        </div>
                      </body>
                    </html>"; 
            return mailBody; 
        }
    }
}
