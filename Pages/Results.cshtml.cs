using CS_IP.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

public class ResultsModel : PageModel
{
    #region Properties
    public List<ClickLog> Logs { get; set; } = new();
    #endregion
    #region Methods

    /// <summary>
    /// Haalt de kliklogs op bij het laden van de pagina.
    /// </summary>
    public void OnGet()
    {
        Logs = ClickTracker.GetLogs().ToList();
    }
    #endregion
}