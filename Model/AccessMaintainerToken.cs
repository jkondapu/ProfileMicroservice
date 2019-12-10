using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMicroservice.Model
{
    public class AccessMaintainerToken
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccessTokenExpiration { get; set; }
        public string RefreshTokenExpiration { get; set; }
        public string Success { get; set; }
        public string RequireSsl { get; set; }
        public string IsPasswordExpired { get; set; }
        public string TenantId { get; set; }
        public string TenantName { get; set; }
    }
}
