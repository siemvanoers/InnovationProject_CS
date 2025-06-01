using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace YourProjectNamespace.Pages.Campaign
{
    public class CreateModel : PageModel
    {
        #region Properties
        [BindProperty]
        public string EmailText { get; set; }

        [BindProperty]
        public string Recipients { get; set; }

        [BindProperty]
        public string Title { get; set; }
        public string FakeLink { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Handler voor GET-verzoeken. Zet de standaard trackinglink.
        /// </summary>
        public void OnGet()
        {
            FakeLink = Url.Page("/Track", null, new { campaign = "Innovatieproject" }, Request.Scheme);
        }


        /// <summary>
        /// Handler voor POST-verzoeken. Valideert invoer en verstuurt e-mails naar alle ontvangers.
        /// </summary>
        /// <returns>Redirect naar dezelfde pagina met succesmelding of foutmelding.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var recipients = Recipients.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

            if (recipients.Any(r => !emailRegex.IsMatch(r.Trim())))
            {
                ModelState.AddModelError("Recipients", "Vul alleen geldige e-mailadressen in (één per regel).");
                return Page();
            }

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("localhost", 25, MailKit.Security.SecureSocketOptions.None);

                foreach (var recipient in recipients)
                {
                    var userId = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(recipient.Trim()));
                    var trackUrl = Url.Page("/Track", null, new { campaign = Title, user = userId }, Request.Scheme);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Campagne Test", "test@example.com"));
                    message.To.Add(new MailboxAddress("Test Ontvanger", recipient.Trim()));
                    message.Subject = "Testmail campagne";
                    message.Body = new TextPart("html")
                    {
                        Text = $"{EmailText}<br><a href=\"{trackUrl}\">{trackUrl}</a>"
                    };

                    await client.SendAsync(message);
                }

                await client.DisconnectAsync(true);
            }
            TempData["Success"] = "Testmails verstuurd!";

            return RedirectToPage();
        }
        #endregion
    }
}