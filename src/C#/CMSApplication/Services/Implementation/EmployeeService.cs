using System.Text;
using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.Models.BindingModel;
using CMSApplication.Models.DTO;
using CMSApplication.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DBContext _context;
        private readonly UserManager<User> _userManager;

        public EmployeeService(DBContext context,UserManager<User> userManager )
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task AddEmployees(List<EmployeeDTO> employees)
        {
            try
            {
                List<Employee> objs = new List<Employee>();

                foreach (var item in employees)
                {
                    var user = new User()
                    {
                        Email = item.Email,
                        UserName = item.Email,
                        EmailConfirmed = true,
                        FullName = item.FirstName + " " + item.LastName,
                        NormalizedEmail = item.Email.Normalize(),
                    };

                    var result = await _userManager.CreateAsync(user, "123456789");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(user, Enums.UserRole.User.ToString());

                    Employee obj = new Employee()
                    {
                        Id = user.Id,
                        EmployeeId = item.EmployeeId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        ContactNo = item.ContactNo,
                        BaseLocation = item.BaseLocation,
                        CareerLevel = item.CareerLevel,
                        Technology = item.Technology,
                        PrimarySkill = item.PrimarySkill,
                        SecondarySkill = item.SecondarySkill,
                        CreatedDate = DateTime.Now,
                    };

                    user.Employee = obj;

                    obj.Workings = new List<Working>();
                    obj.Workings.Add(new Working()
                    {
                        EmployeeId = obj.Id,
                        RoleOffDate = item.RoleOffDate,
                        RoleOnDate = item.RoleOnDate
                    });
                    objs.Add(obj);
                }

                await _context.Employees.AddRangeAsync(objs);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RateEmployee(EmployeeRatingDto dto)
        {
            try
            {
                var obj = await _context.Employees.Where(x => x.Id == dto.Id).FirstOrDefaultAsync();
                if (obj == null) throw new Exception("Employee does not exist");

                obj.Rating = dto.Rating;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task InternalEvalution(InternalEvalution dto)
        {
            try
            {
                var obj = await _context.Employees.Where(x => x.Id == dto.Id).FirstOrDefaultAsync();
                if (obj == null) throw new Exception("Employee does not exist");

                var internalEva = await _context.InternalEvaluations.Where(x => x.EmployeeId == obj.Id).FirstOrDefaultAsync();
                if (internalEva != null)
                {
                    internalEva.Description = dto.Description;
                    _context.Entry(internalEva).State = EntityState.Modified;
                }
                else
                {
                    internalEva = new InternalEvaluation()
                    {
                        EmployeeId = obj.Id,
                        Description = dto.Description,
                        IsDone = true
                    };
                    _context.Entry(internalEva).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async Task<string> GetEmployeeId(int id)
        {
            var obj = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (obj == null) throw new Exception("Employee does not exist");
            return obj.EmployeeId;
        }

        public async Task<string> GetEmployeeProfile(int id)
        {
            var obj = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (obj == null) throw new Exception("Employee does not exist");
            return obj.ProfileFile;
        }

        public async Task UpdateProfilePPTName(int Id, string name)
        {
            try
            {
                var obj = await _context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (obj == null) throw new Exception("Employee does not exist");

                obj.ProfileFile = name;
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async Task<string> GetProfilePPTName(int Id)
        {
            try
            {
                var obj = await _context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (obj == null) throw new Exception("Employee does not exist");

                return obj.ProfileFile;
            }
            catch { throw; }
        }

        public async Task<PagedModel<EmployeeDTO>> GetEmployees(EmployeeFilter filters)
        {
            var listQueryable = _context.Employees.Include(x => x.Workings).AsQueryable();
            if (!string.IsNullOrEmpty(filters.EmpId))
            {
                listQueryable = listQueryable.Where(x => x.EmployeeId.ToLower() == filters.EmpId);
            }

            if (!string.IsNullOrEmpty(filters.Email))
            {
                listQueryable = listQueryable.Where(x => x.Email.ToLower() == filters.EmpId);
            }
            var list = await listQueryable.PaginateAsync(filters.Page, filters.Limit);

            return new PagedModel<EmployeeDTO>()
            {
                CurrentPage = list.CurrentPage,
                PageSize = list.PageSize,
                TotalItems = list.TotalItems,
                TotalPages = list.TotalPages,
                Items = list.Items.Select(x => new EmployeeDTO()
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Technology = x.Technology,
                    PrimarySkill = x.PrimarySkill,
                    CareerLevel = x.CareerLevel,
                    SecondarySkill = x.SecondarySkill,
                    ContactNo = x.ContactNo,
                    BaseLocation = x.BaseLocation,
                    RoleOnDate = x.Workings.FirstOrDefault().RoleOnDate,
                    RoleOffDate = x.Workings.FirstOrDefault().RoleOffDate
                }).ToList()
            };
        }

    }
}
