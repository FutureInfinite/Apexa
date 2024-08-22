namespace Apexa.Services
{
    using Apexa.DataAccess.Repositories.Interfaces;
    using Apexa.Services.Interfaces;
    using Mapster;
    using DC = Apexa.DataContracts;
    using Entity = Apexa.DataAccess.Context.Entities;

    public class AdvisorService : IAdvisorService
    {
        #region Properties&Attributes
        private readonly IAdvisorRepository Repository;
        #endregion Properties&Attributes

        #region Lifetime
        public AdvisorService(IAdvisorRepository Repository)
        {
            this.Repository = Repository;
        }
        #endregion Lifetime

        #region Operations
        /// <summary>
        /// Return an Advisor via the general access
        /// context defintion
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        public async Task<DC.Advisor?> GetAdvisor(string SIN)
        {
            Entity.Advisor? DBAdvisor;

            DBAdvisor = await Repository.GetAdvisor(SIN);
            return DBAdvisor.Adapt<DC.Advisor>();
        }

        /// <summary>
        /// Create a advisor using a general access
        /// context definition
        /// </summary>
        /// <param name="Adv"></param>
        /// <returns></returns>
        public async Task CreateAdvisor(DC.Advisor Adv)
        {
            Entity.Advisor? DBAdvisor;

            DBAdvisor = Adv.Adapt<Entity.Advisor>();
            await Repository.CreateAdvisor(DBAdvisor);
        }

        /// <summary>
        /// Delete the specified Advisor
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdvisor(string SIN)
        {
            return await Repository.DeleteAdvisor(SIN);
        }

        /// <summary>
        /// Save/Update an advisor using a general context
        /// definition
        /// </summary>
        /// <param name="UpdateAdvisor"></param>
        /// <returns></returns>
        public async Task<bool> SaveAdvisor(DC.Advisor UpdateAdvisor)
        {
            Entity.Advisor? DBAdvisor;

            DBAdvisor = UpdateAdvisor.Adapt<Entity.Advisor>();
            return await Repository.SaveAdvisor(DBAdvisor);
        }

        /// <summary>
        /// Get all the DB advisors converted to the
        /// context data objects
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DC.Advisor>> GetAdvisors()
        {
            IEnumerable<Entity.Advisor> DBAdvisors = await Repository.GetAdvisors();
            return DBAdvisors.Adapt<IEnumerable<DC.Advisor>>();
        }
        #endregion Operations
    }
}
