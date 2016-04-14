using MVCWebAPI.Ninject;

namespace MVCWebAPI
{
    /// <summary>
    /// The Ninject config.
    /// </summary>
    public static class PreStart
    {
        /// <summary>
        /// Starts initialisation of the Ninject bindings.
        /// </summary>
        public static void Start()
        {
            NinjectCommon.Start();
        }

        /// <summary>
        /// Destroys the Ninject bindings.
        /// </summary>
        public static void Stop()
        {
            NinjectCommon.Stop();
        }
    }
}