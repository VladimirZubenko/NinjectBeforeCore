using System.Web.Http;

namespace MVCWebAPI.Ninject
{
    /// <summary>
    /// The startup config.
    /// </summary>
    public class StartupConfig
    {
        private static HttpConfiguration _config;

        /// <summary>
        /// Gets the config.
        /// </summary>
        public static HttpConfiguration Config => _config ?? (_config = new HttpConfiguration());
    }
}
