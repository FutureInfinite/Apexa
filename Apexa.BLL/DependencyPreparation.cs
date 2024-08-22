using Apexa.Services.Interfaces;
using Apexa.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Apexa.BLL.Interfaces;

namespace Apexa.BLL
{
    public class DependencyPreparation
    {
        #region Operations
        /// <summary>
        /// prepare dependency injection configuration for
        /// busniess layer
        /// </summary>
        /// <param name="Container"></param>
        public static void PrepareDependencies(IServiceCollection Collection)
        {
            //also setup dependecy injection configuration for services            
            Apexa.Services.DependencyPreparation.PrepareDependencies(Collection);

            Collection.TryAddSingleton<IAdvisorAPILogic, AdvisorAPILogic>();
        }
        #endregion Operations
    }
}
