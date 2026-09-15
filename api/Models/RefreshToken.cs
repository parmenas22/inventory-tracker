using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class RefreshToken: BaseModel
    {
        public Guid RefreshTokenId { get; set; }
        public Guid UserId { get; set; }
        public required string HashRefreshToken { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public Guid FamilyId { get; set; }
        public string? DeviceId { get; set; }
        public string? IPAddress { get; set; }
        public User? User {get; set;}
    }
}