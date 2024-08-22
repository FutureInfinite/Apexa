using Apexa.Services.Interfaces;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apexa.Services
{
    public class DependencyPreparation
    {        
        /// <summary>
        /// prepare dependency injection configuration for
        /// services
        /// </summary>
        /// <param name="Container"></param>
        public static void PrepareDependencies(IServiceCollection Collection)
        {
            //also setup dependecy injection configuration for data manager
            //services is main access point to data layer
            Apexa.DataAccess.DependencyPreparation.PrepareDependencies(Collection);

            Collection.TryAddSingleton<IAdvisorService, AdvisorService>();
        }
    }
}
