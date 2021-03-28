using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace engine_plugin_backend.Models
{
    public class AuthServerOptions
    {
        public const string ISSUER = "AuthServer";
        public const string AUDIENCE = "Recrutan";
        const string KEY = "recruTan_key_testing_test";
        public const int LIFETIME = 60; // token life-tiem 60 minutes
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}