using Apexa.DataAccess.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apexa.DataAccess.Context.Interfaces
{
    internal interface IApexaContext
    {
        #region Properties&Attributes
        DbSet<Advisor> Advisors { get; set; }        
        #endregion Properties&Attributes

        #region Operations
        Task<int> SaveChangesAsynch();
        void ClearAdvisorsState(Advisor Adv);
        #endregion Operations
    }
}
