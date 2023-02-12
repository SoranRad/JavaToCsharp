using ClosedXML.Excel;
using CMSApplication.Enums;
using CMSApplication.Models.DTO;
using CMSApplication.Services.Abstraction;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CMSApplication.Services.Implementation
{
    public class ExcelService : IExcelService
    {
        public List<EmployeeDTO> ReadEmployeeExcel(Stream stream)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            using (XLWorkbook workBook = new XLWorkbook(stream))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                var rowCount = workBook.Worksheet(1).LastRowUsed().RowNumber();
                var columnCount = workBook.Worksheet(1).LastColumnUsed().ColumnNumber();
                int row = 2;
                string careerLevel;
                string roleOnDate;
                string roleOffDate;
                while (row <= rowCount)
                {
                    EmployeeDTO obj = new EmployeeDTO();
                    obj.EmployeeId = workBook.Worksheets.Worksheet(1).Cell(row, 1).GetString();
                    obj.FirstName = workBook.Worksheets.Worksheet(1).Cell(row, 2).GetString();
                    obj.LastName = workBook.Worksheets.Worksheet(1).Cell(row, 3).GetString();
                    obj.Email = workBook.Worksheets.Worksheet(1).Cell(row, 4).GetString();
                    obj.ContactNo = workBook.Worksheets.Worksheet(1).Cell(row, 5).GetString();
                    obj.BaseLocation = workBook.Worksheets.Worksheet(1).Cell(row, 6).GetString();
                    careerLevel = workBook.Worksheets.Worksheet(1).Cell(row, 7).GetString();
                    obj.CareerLevel = Enum.Parse<CareerLevel>(careerLevel).GetHashCode();
                    obj.Technology = workBook.Worksheets.Worksheet(1).Cell(row, 8).GetString();
                    obj.PrimarySkill = workBook.Worksheets.Worksheet(1).Cell(row, 9).GetString();
                    obj.SecondarySkill = workBook.Worksheets.Worksheet(1).Cell(row, 10).GetString();
                    roleOnDate = workBook.Worksheets.Worksheet(1).Cell(row, 11).GetString();

                    if (DateTime.TryParse(roleOnDate, out DateTime RoleOndateTime))
                    {
                        obj.RoleOnDate = RoleOndateTime;
                    }

                    roleOffDate = workBook.Worksheets.Worksheet(1).Cell(row, 12).GetString();

                    if (DateTime.TryParse(roleOffDate, out DateTime RoleOffDate))
                    {
                        obj.RoleOffDate = RoleOffDate;
                    }
                    list.Add(obj);
                    row++;
                }
            }
            return list;
        }
    }
}
