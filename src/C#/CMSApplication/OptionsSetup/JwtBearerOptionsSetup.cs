using CMSApplication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CMSApplication.OptionsSetup
{
    public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JWTConfig _jwtConfig;

        public JwtBearerOptionsSetup(IOptions<JWTConfig> jwtconfig) {


            _jwtConfig = jwtconfig.Value;
        
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters.ValidIssuer = _jwtConfig.Issuer;
            options.TokenValidationParameters.ValidAudience = _jwtConfig.Audience;
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        }
    }

}
