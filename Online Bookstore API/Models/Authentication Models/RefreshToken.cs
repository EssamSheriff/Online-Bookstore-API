using Microsoft.EntityFrameworkCore;

namespace Online_Bookstore_API.Models
{
    [Owned]
    public class RefreshToken
    {
        public String Token { get; set; }

        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreateOn { get; set; }
        public DateTime? RevokeOn { get; set; }
        public bool IsActive => RevokeOn == null && !IsExpired;
    }
}
