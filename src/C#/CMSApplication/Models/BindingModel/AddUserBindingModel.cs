namespace CMSApplication.Models.BindingModel
{
    public class AddUserBindingModel
    {

        public string FullName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public List<string> Roles { get; set; }


    }
}
