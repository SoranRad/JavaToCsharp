using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CMSApplication.Models.DTO
{
    public class UserDTO
    {

        public UserDTO(string fullName,string email, string userName, DateTime dateCreated , List<string> roles)
        {
            FullName = fullName;
            Email = email;
            UserName = userName;
            DateCreated = dateCreated;
            Roles = roles;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public DateTime DateCreated { get; private set; }

        public string Token { get; set; }

        public List<string> Roles { get; private set; }
    }
}
