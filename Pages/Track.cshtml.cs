using CS_IP.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class TrackModel : PageModel
{

    #region Properties
    public string? Campaign { get; set; }
    public string? User { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Handeld de GET-aanroep voor de Track-pagina.
    /// </summary>
    /// <param name="campaign"></param>
    /// <param name="user"></param>
    public void OnGet(string? campaign, string? user)
    {
        Campaign = campaign ?? "Onbekend";
        User = user ?? "Onbekend";
        ClickTracker.RegisterClick(Campaign, User);
    }
    #endregion
}