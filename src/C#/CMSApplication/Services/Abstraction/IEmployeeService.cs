using CMSApplication.Models.BindingModel;
using CMSApplication.Models.DTO;

namespace CMSApplication.Services.Abstraction
{
    public interface IEmployeeService
    {
        Task AddEmployees(List<EmployeeDTO> employees);
        Task<string> GetEmployeeId(int id);
        Task<string> GetEmployeeProfile(int id);
        Task<PagedModel<EmployeeDTO>> GetEmployees(EmployeeFilter filters);
        Task<string> GetProfilePPTName(int Id);
        Task InternalEvalution(InternalEvalution dto);
        Task<bool> RateEmployee(EmployeeRatingDto dto);
        Task UpdateProfilePPTName(int Id, string name);
    }
}
