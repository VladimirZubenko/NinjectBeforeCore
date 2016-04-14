using System;
using System.Web;
using System.Web.Http.Dependencies;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

namespace MVCWebAPI.Ninject
{
    /// <summary>
    /// The Ninject web common.
    /// </summary>
    public static class NinjectCommon
    {
        /// <summary>
        /// The boot start.
        /// </summary>
        public static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static IKernel Kernel
        {
            get
            {
                return Bootstrapper.Kernel;
            }
        }

        public static IDependencyResolver ServciceLocator
        {
            get
            {
                if (Bootstrapper.Kernel == null) return null;
                return new MVCWebAPI.Ninject.NinjectDependencyResolver(Bootstrapper.Kernel);
            }
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(() => CreateKernel());
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                var config = StartupConfig.Config;
                config.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch (Exception ex)
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<BusinessValueProviders.IBusinessValueProvider>().To<BusinessValueProviders.BusinessValueProvider>().InRequestScope();
        }
    }
}
