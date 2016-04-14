using MVCWebAPI.Ninject;

namespace MVCWebAPI.Tests
{
    public static class Given_dependency_resolver
    {
        public static void GivenDependencyResolver(this IntegrationTestsContext context)
        {
            NinjectCommon.Start();

            context.Kernel = NinjectCommon.Bootstrapper.Kernel;
        }
    }
}
