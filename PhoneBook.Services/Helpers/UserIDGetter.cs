using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.Helpers
{
    public class UserIDGetter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIDGetter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserIDGetter()
        {
       
        }

        public string getUserIDByToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
            return new JwtSecurityToken(token).Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        }
    }
}
