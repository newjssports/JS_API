using System;
using System.Collections.Generic;

namespace SportsOrderApp.Entities
{
    public partial class JsTblOrderRequestLog
    {
        public long OrderRequestLogId { get; set; }
        public long? OrderRequestId { get; set; }
        public long? UserId { get; set; }
        public string? Status { get; set; }
        public string? OrderType { get; set; }
        public bool? IsApproved { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public string? Attribute1 { get; set; }
        public string? Attribute2 { get; set; }
        public string? Attribute3 { get; set; }
        public string? Attribute4 { get; set; }
        public string? Attribute5 { get; set; }

        public virtual JsTblOrderRequest? OrderRequest { get; set; }
        public virtual JsTblUser? User { get; set; }
    }
}
