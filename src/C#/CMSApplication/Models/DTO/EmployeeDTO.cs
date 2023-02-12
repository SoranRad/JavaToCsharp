using System.ComponentModel.DataAnnotations;

namespace CMSApplication.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string BaseLocation { get; set; }
        public int CareerLevel { get; set; }
        public string CareerLevelStr { get; set; }
        [Required]
        public string Technology { get; set; }
        [Required]
        public string PrimarySkill { get; set; }
        [Required]
        public string SecondarySkill { get; set; }
        public DateTime RoleOnDate { get; set; }
        public DateTime RoleOffDate { get; set; }
    }
}
