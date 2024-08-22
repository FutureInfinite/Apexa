using Apexa.DataAccess.Context;
using Apexa.DataAccess.Context.Interfaces;
using Apexa.DataAccess.Repositories;
using Apexa.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Apexa.DataAccess
{
    public class DependencyPreparation
    {
        #region Operations
        /// <summary>
        /// Prepare DI for dynamic instantiation
        /// </summary>
        /// <param name="Container"></param>
        public static void PrepareDependencies(IServiceCollection Collection)
        {
            Collection.AddDbContext<IApexaContext,ApexaContext>();            
            Collection.TryAddSingleton<IAdvisorRepository, AdvisorRepository>();
        }               
        #endregion Operations
    }
}
