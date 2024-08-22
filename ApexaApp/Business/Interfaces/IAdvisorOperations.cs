using ApexaApp.Business.DataContracts;
using Support;

namespace ApexaApp.Business.Interfaces
{
    public interface IAdvisorOperations
    {
        Task<IEnumerable<Advisor>?> GetAdvisors();
        Task<Response> CreateAdvisor(string SIN, string Name, string Address, string Phone);
        Task<Advisor>? GetAdvisor(string SIN);
        Task<Response> UpdateAdvisor(string SIN, string Name, string Address, string Phone);
        Task<bool> DeleteAdvisor(string SIN);
    }
}
