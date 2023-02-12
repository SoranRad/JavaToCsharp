using Microsoft.AspNetCore.Identity;

namespace CMSApplication.Data.Entity
{
    public class ApplicationRole : IdentityRole<int>, IBaseEntity<int>
    {
        object IBaseEntity.Id
        {
            get => this.Id;
            set => Id = (int)Convert.ChangeType(value, typeof(int));
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
