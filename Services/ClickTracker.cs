using System.Collections.Concurrent;

namespace CS_IP.Services
{
    public class ClickLog
    {
        #region Properties
        public string Campaign { get; set; } = "";
        public string User { get; set; } = "";
        public DateTime Timestamp { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor voor het aanmaken van een nieuwe ClickLog instantie.
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="user"></param>
        /// <param name="timestamp"></param>
        public ClickLog(string campaign, string user, DateTime timestamp)
        {
            Campaign = campaign;
            User = user;
            Timestamp = timestamp;
        }
        #endregion
    }

    #region Methods
    /// <summary>
    /// Functionaliteit om kliklogs bij te houden voor campagnes.
    /// </summary>
    public static class ClickTracker

    {
        private static readonly ConcurrentBag<ClickLog> _logs = new();

        public static void RegisterClick(string campaign, string user)
        {
            _logs.Add(new ClickLog(
                campaign ?? "Onbekend",
                user ?? "Onbekend",
                DateTime.Now
            ));
        }

        /// <summary>
        /// Haalt alle kliklogs op.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ClickLog> GetLogs() => _logs;
    }
    #endregion
}
