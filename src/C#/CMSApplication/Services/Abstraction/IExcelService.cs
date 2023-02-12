using CMSApplication.Models.DTO;

namespace CMSApplication.Services.Abstraction
{
    public interface IExcelService
    {
        List<EmployeeDTO> ReadEmployeeExcel(Stream stream);
    }
}
