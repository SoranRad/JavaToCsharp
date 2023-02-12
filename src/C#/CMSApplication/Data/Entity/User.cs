using Microsoft.AspNetCore.Identity;

namespace CMSApplication.Data.Entity
{
    public class User : IdentityUser<int>, IBaseEntity<int>
    {

        public string FullName { get; set; }
         
        object IBaseEntity.Id
        {
            get => this.Id;
            set => Id = (int)Convert.ChangeType(value, typeof(int));
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<UserRole>    UserRoles   { get; set; }
        public ICollection<Scores>      UserScores  { get; set; }

        public Employee     Employee { get; set; }
    }
}
