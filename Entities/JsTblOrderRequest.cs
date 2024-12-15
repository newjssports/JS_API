using System;
using System.Collections.Generic;

namespace SportsOrderApp.Entities
{
    public partial class JsTblOrderRequest
    {
        public JsTblOrderRequest()
        {
            JsTblOrderRequestLogs = new HashSet<JsTblOrderRequestLog>();
        }

        public long OrderRequestId { get; set; }
        public long? MockupId { get; set; }
        public bool? IsMockupReference { get; set; }
        public string? TeamName { get; set; }
        public long? MainCategoryId { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? ProductId { get; set; }
        public long? FabricTypeId { get; set; }
        public long? NeckStyleId { get; set; }
        public string? Note { get; set; }
        public string? OrderStatus { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? UserId { get; set; }
        public string? Attribute1 { get; set; }
        public string? Attribute2 { get; set; }
        public string? Attribute3 { get; set; }
        public string? Attribute4 { get; set; }
        public string? Attribute5 { get; set; }
        public string? Attribute6 { get; set; }
        public string? Attribute7 { get; set; }
        public string? Attribute8 { get; set; }
        public string? Attribute9 { get; set; }
        public string? Attribute10 { get; set; }
        public int? Attribute11 { get; set; }
        public int? Attribute12 { get; set; }
        public int? Attribute13 { get; set; }
        public int? Attribute14 { get; set; }
        public int? Attribute15 { get; set; }
        public DateTime? Attribute16 { get; set; }
        public DateTime? Attribute17 { get; set; }
        public DateTime? Attribute18 { get; set; }
        public string? Attribute19 { get; set; }
        public string? Attribute20 { get; set; }
        public bool? IsNewOrder { get; set; }
        public string? OrderType { get; set; }
        public long? OrderRequestNo { get; set; }

        public virtual JsTblCategory? Category { get; set; }
        public virtual JsTblMainCategory? MainCategory { get; set; }
        public virtual JsTblMockup? Mockup { get; set; }
        public virtual JsTblProduct? Product { get; set; }
        public virtual JsTblSubCategory? SubCategory { get; set; }
        public virtual JsTblUser? User { get; set; }
        public virtual ICollection<JsTblOrderRequestLog> JsTblOrderRequestLogs { get; set; }
    }
}
