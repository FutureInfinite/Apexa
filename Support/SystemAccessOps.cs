using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.ConstrainedExecution;

namespace Support
{
    public class SystemAccessOps
    {
        #region Properties&Attributes
        public static IServiceCollection Services { get; private set; }
        public static ServiceProvider Provider { get; private set; }
        #endregion Properties&Attributes

        static SystemAccessOps()
        {
            PrepareDIConfig();
        }
        private static void PrepareDIConfig()
        {
            if (Services == null) Services = new ServiceCollection();
        }

        public static void BuildServices()
        {
            Provider = Services.BuildServiceProvider();
        }

        
    }
}
