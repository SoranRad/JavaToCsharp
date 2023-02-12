using System.ComponentModel.DataAnnotations;

namespace CMSApplication.Data.Entity
{
    public class Employee : BaseEntity<int>
    {
        [Required]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string BaseLocation { get; set; }
        public int CareerLevel { get; set; }
        public string Technology { get; set; }
        public string PrimarySkill { get; set; }
        public string SecondarySkill { get; set; }
        public string? Feedback { get; set; }
        public int Rating { get; set; }
        public string? ProfileFile { get; set; }

        public User User { get; set; }

        public virtual ICollection<Working> Workings { get; set; }
        public virtual ICollection<InternalEvaluation> InternalEvaluations { get; set; }
        public virtual ICollection<ClientEvaluation> ClientEvaluations { get; set; }

    }
}
