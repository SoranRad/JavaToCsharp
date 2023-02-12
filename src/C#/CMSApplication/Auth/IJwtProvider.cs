using CMSApplication.Data.Entity;

namespace CMSApplication.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user, List<string> roles);
    }
}
