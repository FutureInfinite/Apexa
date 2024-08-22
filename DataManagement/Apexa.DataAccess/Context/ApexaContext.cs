using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Apexa.DataAccess.Context.Interfaces;
using Apexa.DataAccess.Context.Entities;

namespace Apexa.DataAccess.Context
{
    internal class ApexaContext : DbContext, IApexaContext
    {
        #region Properties&Attributes
            protected IConfiguration Configuration { get;  }            
            public virtual DbSet<Advisor> Advisors { get; set; }
        #endregion Properties&Attributes

        #region Lifetime
        public ApexaContext()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();            
        }
        #endregion Lifetime

        #region Operations
        /// <summary>
        /// Prepare the in memeory DB with the name of
        /// ApexaInMemoryDb
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("ApexaInMemoryDb");
        }

        /// <summary>
        /// Save Changes back to DB
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsynch() => await base.SaveChangesAsync();

        /// <summary>
        /// reset state so previous fails do not
        /// get retripped
        /// </summary>
        public void ClearAdvisorsState(Advisor Adv)
        {
            Entry(Adv).State = EntityState.Detached;            
        }
        #endregion Operations
    }
}
