using System.ComponentModel.DataAnnotations;

namespace CMSApplication.Models.DTO
{
    public class EmployeeRatingDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
    }

    public class InternalEvalution
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }

    public class EmployeeFilter
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string EmpId { get; set; }
        public string Email { get; set; }
        public DateTime? RoleOnDate { get; set; }
        public DateTime? RoleOffDate { get; set; }
    }

    public class ResponseDto
    {
        public string Message { get; set; }
    }

}
