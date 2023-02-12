using System.ComponentModel.DataAnnotations;

namespace CMSApplication.Data.Entity
{
    public class InternalEvaluation : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public bool IsDone { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}
