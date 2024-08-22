using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC = Apexa.DataContracts;

namespace Apexa.Services.Interfaces
{
    public interface IAdvisorService
    {
        #region Operiations
        Task<DC.Advisor?> GetAdvisor(string SIN);
        Task CreateAdvisor(DC.Advisor Adv);
        Task<bool> DeleteAdvisor(string SIN);
        Task<bool> SaveAdvisor(DC.Advisor UpdateAdvisor);
        Task<IEnumerable<DC.Advisor>> GetAdvisors();
        #endregion Operiations
    }
}
