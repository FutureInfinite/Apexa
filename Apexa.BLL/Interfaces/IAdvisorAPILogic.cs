using Apexa.Services;
using Apexa.DataContracts;

namespace Apexa.BLL.Interfaces
{
    public interface IAdvisorAPILogic
    {

        #region Operations
        Task<Advisor?> GetAdvisor(string SIN);
        Task CreateAdvisor(Advisor Adv);
        Task<bool> DeleteAdvisor(string SIN);
        Task<bool> SaveAdvisor(Advisor UpdateAdvisor);
        Task<IEnumerable<Advisor>> GetAdvisors();
        #endregion Operations
    }
}
