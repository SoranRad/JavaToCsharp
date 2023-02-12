namespace CMSApplication.Data.Entity
{
    public class Working :BaseEntity<int>
    {

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime RoleOnDate { get; set; }
        public DateTime RoleOffDate { get; set; }   


    }
}
