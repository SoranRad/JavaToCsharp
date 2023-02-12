using CMSApplication.Models.DTO;
using CMSApplication.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IExcelService _excelService;
        private readonly IFileService _fileService;


        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IExcelService excelService, IFileService fileService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _excelService = excelService;
            _fileService = fileService;
        }

        [HttpPost("/Excel")]
        public async Task<IActionResult> UploadExcel([Required] IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    var fileType = Path.GetExtension(file.FileName);
                    if (fileType == ".xlsx")
                    {
                        var list = _excelService.ReadEmployeeExcel(file.OpenReadStream());
                        await _employeeService.AddEmployees(list);
                        return Ok(new ResponseDto() { Message = "File Uploaded" });
                    }
                    else
                    {
                        return BadRequest(new ResponseDto() { Message = "Please upload .xlsx file" });
                    }
                }
                else
                {
                    return BadRequest(new ResponseDto() { Message = "Invalid file" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("/Rating")]
        public async Task<IActionResult> EmployeeRating([FromBody] EmployeeRatingDto dto)
        {
            if (dto.Id <= 0) return BadRequest(new ResponseDto() { Message = "Id must be greater zero" });
            try
            {
                var result = await _employeeService.RateEmployee(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ResponseDto() { Message = "Something went wrong" });
            }
        }

        [HttpPost("/InternalEvalution")]
        public async Task<IActionResult> InternalEvalution([FromBody] InternalEvalution dto)
        {
            if (dto.Id <= 0) return BadRequest(new ResponseDto() { Message = "Id must be greater zero" });
            try
            {
                await _employeeService.InternalEvalution(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ResponseDto() { Message = "Something went wrong" });
            }
        }

        [HttpPost("/Profile")]
        public async Task<IActionResult> UploadProfile(int Id, IFormFile file)
        {
            var fileType = Path.GetExtension(file.FileName);
            if (fileType.ToLower() == ".pptx")
            {
                string empId = await _employeeService.GetEmployeeId(Id);
                string fileName = await _fileService.PostFile(file, empId);
                await _employeeService.UpdateProfilePPTName(Id, fileName);
                return Ok();
            }
            else
            {
                return BadRequest(new ResponseDto() { Message = "File is not proper" });
            }
        }

        [HttpGet("/Profile")]
        public async Task<IActionResult> DownloadProfile(int Id)
        {
            string name = await _employeeService.GetProfilePPTName(Id);
            byte[] byteArray = await _fileService.DownloadFile(name);
            return File(byteArray, "application/vnd.openxmlformats-officedocument.presentationml.presentation", "profile.pptx");
        }

        [HttpGet("/Employees")]
        public async Task<IActionResult> GetEmployeeList([FromQuery] UrlQueryParameters dto)
        {
            EmployeeFilter filter = new EmployeeFilter()
            {
                EmpId = dto.EmpId,
                Limit = dto.Limit,
                Page = dto.Page,
                Email = dto.email,
                RoleOffDate = dto.RoleOffDate,
                RoleOnDate = dto.RoleOnDate
            };
            return Ok(await _employeeService.GetEmployees(filter));
        }

        public record UrlQueryParameters(int Limit = 10, int Page = 1, string EmpId = "", string email = "", DateTime? RoleOnDate = null, DateTime? RoleOffDate = null);


    }
}
