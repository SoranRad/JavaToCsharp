using Microsoft.AspNetCore.Identity;

namespace CMSApplication.Data.Entity
{
    public class UserRole : IdentityUserRole<int>, IBaseEntity<int>
    {
        object IBaseEntity.Id
        {
            get => Id;
            set => Id = (int)Convert.ChangeType(value, typeof(int));
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public User             userRole { get; set; }
        public ApplicationRole  AppRole { get; set; }
    }
}
