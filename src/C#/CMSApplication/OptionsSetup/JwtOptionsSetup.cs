using CMSApplication.Models;
using Microsoft.Extensions.Options;

namespace CMSApplication.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JWTConfig>
    {

        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JWTConfig options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }

     
    }
}
