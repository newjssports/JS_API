using System;
using System.Collections.Generic;

namespace SportsOrderApp.Entities
{
    public partial class JsTblVerificationCode
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Code { get; set; } = null!;
        public string CodeType { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public bool IsUsed { get; set; }

        public virtual JsTblUser User { get; set; } = null!;
    }
}
