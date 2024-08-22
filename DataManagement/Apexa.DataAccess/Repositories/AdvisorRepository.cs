using Apexa.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Apexa.DataAccess.Context.Entities;
using Apexa.DataAccess.Context.Interfaces;
using Caliburn.Micro;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;

namespace Apexa.DataAccess.Repositories
{
    internal class AdvisorRepository : IAdvisorRepository
    {
        #region Propeties&Attributes
        private DbSet<Advisor> Advisors;
        private readonly IApexaContext Context;
        #endregion Propeties&Attributes

        #region Lifetime
        public AdvisorRepository(IApexaContext Context)
        {
            this.Context = Context;
            this.Advisors = Context.Advisors;
        }
        #endregion Lifetime

        #region Operations
        /// <summary>
        /// Retireve the Advisor with
        /// the specified SSN
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        public async Task<Advisor?> GetAdvisor(string SIN)
        {
            Advisor? Result;

            IQueryable<Advisor> InitialQuery = Advisors.Where(advisor => advisor.SIN.ToLower().Trim().Equals(SIN.ToLower().Trim()));

            Result = await InitialQuery.FirstOrDefaultAsync();

            if (Result != null) Context.ClearAdvisorsState(Result);

            return Result;
        }
        
        /// <summary>
        /// Add the given advisor to the DB
        /// </summary>
        /// <param name="Adv"></param>
        /// <returns></returns>
        public async Task CreateAdvisor(Advisor Adv)
        {
            List<ValidationResult> validationResults;
            ValidationContext contexts;
            bool isValid;

            try
            {
                //randonly set the health field
                Adv.HealthStatus = PrepareHealthStatus();
                
                validationResults = new List<ValidationResult>();                
                contexts = new ValidationContext(Adv, null, null);
                isValid = Validator.TryValidateObject(Adv, contexts, validationResults, true);

                if (isValid)
                {
                    var IsUniqueSIN = IsUnique(Adv.SIN);
                    IsUniqueSIN.Wait();
                    isValid = IsUniqueSIN.Result;
                    if (!isValid)
                    {
                        //the specified SIN is not unique
                        throw new Exception("SIN is not unique");
                    }
                }
                else
                {                    
                    throw new Exception(validationResults.Count > 0 ? validationResults[0].ErrorMessage : "");                    
                }

                if (isValid)
                {
                    await Advisors.AddAsync(Adv);
                    await Context.SaveChangesAsynch();
                }
                else
                {
                    throw new Exception(validationResults.FirstOrDefault()?.ErrorMessage);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                Context.ClearAdvisorsState(Adv);
            }
        }

        /// <summary>
        /// remove the specified Advisor    
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdvisor(string SIN)
        {
            bool Result = false;

            Advisor? CurrentAdvisor = await GetAdvisor(SIN);

            if (CurrentAdvisor != null)
            {
                try
                {
                    Advisors.Remove(CurrentAdvisor);
                    await Context.SaveChangesAsynch();
                    Result = true;
                }
                catch (Exception ex)
                {                    
                    throw;
                }
                finally
                {
                    Context.ClearAdvisorsState(CurrentAdvisor);
                }                
            }
            else //specified advisor does not exist
                Result = true;

            return Result;
        }

        public async Task<bool> SaveAdvisor(Advisor UpdateAdvisor)
        {
            bool Result = false;
            List<ValidationResult> validationResults;
            ValidationContext contexts;
            bool isValid;

            Advisor? CurrentAdvisor = await GetAdvisor(UpdateAdvisor.SIN);

            if (CurrentAdvisor != null)
            {
                CurrentAdvisor.Name = UpdateAdvisor.Name;
                CurrentAdvisor.Address = UpdateAdvisor.Address;
                CurrentAdvisor.Phone = UpdateAdvisor.Phone;
                CurrentAdvisor.SIN = UpdateAdvisor.SIN;
                CurrentAdvisor.HealthStatus = UpdateAdvisor.HealthStatus;

                validationResults = new List<ValidationResult>();
                contexts = new ValidationContext(CurrentAdvisor, null, null);
                isValid = Validator.TryValidateObject(CurrentAdvisor, contexts, validationResults, true);
                
                if (isValid)
                {
                    await Context.SaveChangesAsynch();
                    Result = true;
                }                
                else
                {
                    throw new Exception(validationResults.Count > 0 ? validationResults[0].ErrorMessage : "");
                }

                Context.ClearAdvisorsState(CurrentAdvisor);
            }
            else //the specified advisro does not exist - cannot update
                Result = false;

            return Result;
        }

        /// <summary>
        /// retrieval of the all Advisros from the
        /// DB. This is an all operation. This
        /// should be a paginated routine as
        /// grabbing a complete table in one
        /// shot is not considered good practice.
        /// Only doing so in this operation is for
        /// demonstration purposes for the test
        /// </summary>
        /// <returns></returns>        
        public async Task<IEnumerable<Advisor>> GetAdvisors() => await Advisors.ToListAsync();

        private HealthStatus PrepareHealthStatus()
        {
            //Green = 60 % Yellow = 20 % Red = 20 %
            HealthStatus Result = HealthStatus.Green;
            Random RandomHealth = new Random(Guid.NewGuid().GetHashCode());
            int perCent = RandomHealth.Next(0, 100);

            if (perCent >= 0 && perCent <= 59)
            {
                Result = HealthStatus.Green;
            }
            else if(perCent >= 60 && perCent <= 79)
            {
                Result = HealthStatus.Yellow;
            }
            else if (perCent >= 80 && perCent <= 100)
            {
                Result = HealthStatus.Red;
            }

            return Result;
        }


        /// <summary>
        /// Determine if the SSN is unique
        /// </summary>
        /// <param name="sin"></param>
        /// <returns></returns>
        private async Task<bool> IsUnique(string sin)
        {
            bool Unique = false;
            Advisor? Advisor;
            IAdvisorRepository? AdRepo = SystemAccessOps.Provider.GetService<IAdvisorRepository>();

            if (AdRepo != null)
            {
                Advisor = await AdRepo.GetAdvisor(sin);

                if (Advisor != null)
                    Unique = false;
                else
                    Unique = true;
            }
            else
            {
                throw new Exception("Could not access DB");
            }


            return Unique;
        }

        #endregion Operations
    }
}
