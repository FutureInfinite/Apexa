
using Apexa.Services.Interfaces;
using Apexa.DataContracts;
using Apexa.BLL.Interfaces;

namespace Apexa.BLL
{
    internal class AdvisorAPILogic : IAdvisorAPILogic
    {
        #region Properties&Attributes
        IAdvisorService AdvisorService;
        #endregion Properties&Attributes

        #region Lifetime
        public AdvisorAPILogic(IAdvisorService AdvisorService)
        {
            this.AdvisorService = AdvisorService;
        }
        #endregion Lifetime

        #region Operations
        /// <summary>
        /// get speicifc advisor from SIN
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        public async Task<Advisor?> GetAdvisor(string SIN) => await AdvisorService.GetAdvisor(SIN);
        /// <summary>
        /// Add advisor to DB
        /// </summary>
        /// <param name="Adv"></param>
        /// <returns></returns>
        public async Task CreateAdvisor(Advisor Adv) => await AdvisorService.CreateAdvisor(Adv);
        /// <summary>
        /// delete advisor from DB
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdvisor(string SIN) => await AdvisorService.DeleteAdvisor(SIN);
        /// <summary>
        /// Save update advisor to db
        /// </summary>
        /// <param name="UpdateAdvisor"></param>
        /// <returns></returns>
        public async Task<bool> SaveAdvisor(Advisor UpdateAdvisor) => await AdvisorService.SaveAdvisor(UpdateAdvisor);
        /// <summary>
        /// get all advisors from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Advisor>> GetAdvisors() => await AdvisorService.GetAdvisors();    
        #endregion Operations
    }
}
