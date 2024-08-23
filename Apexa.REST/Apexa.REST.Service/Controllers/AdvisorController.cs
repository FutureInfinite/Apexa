using Apexa.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Apexa.DataContracts;

using Support;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apexa.REST.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AdvisorController : ControllerBase
    {
        #region Properties&Attributes
        private readonly IAdvisorAPILogic AdvisorAPI;
        #endregion Properties&Attributes

        #region Lifetime
        public AdvisorController()
        {
            //Constructor DI appears to not work. Other constructors
            //are working - unclear why not here
            this.AdvisorAPI = SystemAccessOps.Provider.GetRequiredService<IAdvisorAPILogic>();
        }
        #endregion Lifetime

        #region Operations
        [HttpGet("Check")]
        [SwaggerOperation("Determine if Service is available")]
        public bool CheckService()
        {
            return true;
        }

        /// <summary>
        /// Get an advisor using a social 
        /// insurance number
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        [HttpGet("Get/{SIN}")]
        [SwaggerOperation("Get specified Advisors from the DB")]
        public async Task<Advisor> GetAdvisor(string SIN)
        {
            return await AdvisorAPI.GetAdvisor(SIN);
        }

        /// <summary>
        /// Creare an advisor with giving params
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        [Route("Create/{SIN}/{Name}/{Address}/{Phone}")]
        [SwaggerOperation("Add specified Advisor to DB")]
        [HttpPost]
        public async Task<Response> CreateAdvisor(
            string SIN,
            string Name,
            string Address,
            string Phone
            )
        {
            Response Resp = new Response();
            
            Advisor NewAdvisor = new Advisor()
            {
                SIN = SIN,
                Name = Name,
                Address = Address,
                Phone = Phone
            };

            try
            {
                await AdvisorAPI.CreateAdvisor(NewAdvisor);
                Resp.Succeeded = true;
            }
            catch (Exception ex)
            {
                Resp.Message = ex.Message;
                Resp.Succeeded = false;                
            }         


            return Resp;
        }

        /// <summary>
        /// Delete the specified Advisor
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{SIN}")]
        [SwaggerOperation("Delete a specified Advisor from DB")]
        public async Task<bool> DeleteAdvisor(string SIN)
        {
            return await AdvisorAPI.DeleteAdvisor(SIN);
        }

        /// <summary>
        /// Update the specified Advisor
        /// </summary>
        /// <param name="SIN"></param>
        /// <param name="Name"></param>
        /// <param name="Address"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        [HttpPut("Update/{SIN}/{Name}/{Address}/{Phone}")]
        [SwaggerOperation("Update identified Advisor in DB")]
        public async Task<Response> SaveAdvisor(
            string SIN,
            string Name,
            string Address,
            string Phone
        )
        {
            Response Result = new Response();
            
            Advisor UpdateAdvisor = new Advisor()
            {
                SIN = SIN,
                Name = Name,
                Address = Address,
                Phone = Phone
            };

            try
            {
                bool SavedOk = await AdvisorAPI.SaveAdvisor(UpdateAdvisor);
                Result.Succeeded = SavedOk;
                if (!SavedOk) Result.Message = "problem occurred updating Advisor";
            }
            catch (Exception ex) 
            {
                Result.Succeeded = false;
                Result.Message = ex.Message;                
            }

            return Result;
        }

        /// <summary>
        /// Get all advisors
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAdvisors")]
        [SwaggerOperation("Get all Advisors from the DB")]
        public async Task<IEnumerable<Advisor>> GetAdvisors()
        {
            return await AdvisorAPI.GetAdvisors();
        }
        #endregion Operations
    }
}
