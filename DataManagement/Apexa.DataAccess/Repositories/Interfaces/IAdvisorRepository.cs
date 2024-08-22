using Apexa.DataAccess.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.DataAccess.Repositories.Interfaces
{
    public interface IAdvisorRepository
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
