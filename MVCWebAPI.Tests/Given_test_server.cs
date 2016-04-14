using Microsoft.Owin.Testing;

namespace MVCWebAPI.Tests
{
    public static class Given_test_server
    {
        private static readonly object Sync = new object();

        private static TestServer instance;

        private static TestServer TestServer
        {
            get
            {
                lock (Sync)
                {
                    return instance ?? (instance = TestServer.Create<Startup>());
                }
            }
        }

        public static void GivenTestServer(this IntegrationTestsContext context)
        {
            context.TestServer = TestServer;
        }
    }
}
