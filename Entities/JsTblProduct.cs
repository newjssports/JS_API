using System;
using System.Collections.Generic;

namespace SportsOrderApp.Entities
{
    public partial class JsTblProduct
    {
        public JsTblProduct()
        {
            JsTblMockups = new HashSet<JsTblMockup>();
            JsTblOrderRequests = new HashSet<JsTblOrderRequest>();
            JsTblPriceLists = new HashSet<JsTblPriceList>();
            JsTblProductSizePriceMasters = new HashSet<JsTblProductSizePriceMaster>();
        }

        public long ProductId { get; set; }
        public long? SubCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool? IsDefault { get; set; }
        public bool? Active { get; set; }
        public string? Status { get; set; }
        public bool? Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsNeck { get; set; }
        public bool? IsFrontDesc { get; set; }
        public bool? IsFrontImage { get; set; }
        public bool? IsBackDesc { get; set; }
        public bool? IsBackImage { get; set; }
        public bool? IsOnlyDesc { get; set; }
        public bool? IsLeftSleeveDesc { get; set; }
        public bool? IsRightSleeveDesc { get; set; }
        public bool? IsLeftSleeveImage { get; set; }
        public bool? IsRightSleeveImage { get; set; }
        public string? FrontImage { get; set; }
        public string? BackImage { get; set; }
        public bool? IsAllow1 { get; set; }
        public bool? IsAllow2 { get; set; }
        public bool? IsAllow3 { get; set; }
        public bool? IsAllow4 { get; set; }
        public bool? IsAllow5 { get; set; }
        public bool? IsAllow6 { get; set; }
        public bool? IsAllow7 { get; set; }
        public bool? IsAllow8 { get; set; }
        public bool? IsAllow9 { get; set; }
        public bool? IsAllow10 { get; set; }
        public bool? IsAllow11 { get; set; }
        public bool? IsAllow12 { get; set; }
        public bool? IsAllow13 { get; set; }
        public bool? IsAllow14 { get; set; }
        public bool? IsAllow15 { get; set; }
        public bool? IsAllow16 { get; set; }
        public bool? IsAllow17 { get; set; }
        public bool? IsAllow18 { get; set; }
        public bool? IsAllow19 { get; set; }
        public bool? IsAllow20 { get; set; }
        public bool? IsAllow21 { get; set; }
        public bool? IsAllow22 { get; set; }
        public bool? IsAllow23 { get; set; }
        public bool? IsAllow24 { get; set; }
        public bool? IsAllow25 { get; set; }
        public bool? IsAllow26 { get; set; }
        public bool? IsAllow27 { get; set; }
        public bool? IsAllow28 { get; set; }
        public bool? IsAllow29 { get; set; }
        public bool? IsAllow30 { get; set; }
        public bool? IsAllow31 { get; set; }
        public bool? IsAllow32 { get; set; }
        public bool? IsAllow33 { get; set; }
        public bool? IsAllow34 { get; set; }
        public bool? IsAllow35 { get; set; }
        public bool? IsAllow36 { get; set; }
        public bool? IsAllow37 { get; set; }
        public bool? IsAllow38 { get; set; }
        public bool? IsAllow39 { get; set; }
        public bool? IsAllow40 { get; set; }
        public bool? IsAllow41 { get; set; }
        public bool? IsAllow42 { get; set; }
        public bool? IsAllow43 { get; set; }
        public bool? IsAllow44 { get; set; }
        public bool? IsAllow45 { get; set; }
        public bool? IsAllow46 { get; set; }
        public bool? IsAllow47 { get; set; }
        public bool? IsAllow48 { get; set; }
        public bool? IsAllow49 { get; set; }
        public bool? IsAllow50 { get; set; }
        public bool? IsAllow51 { get; set; }
        public bool? IsAllow52 { get; set; }
        public bool? IsAllow53 { get; set; }
        public bool? IsAllow54 { get; set; }
        public bool? IsAllow55 { get; set; }
        public bool? IsAllow56 { get; set; }
        public bool? IsAllow57 { get; set; }
        public bool? IsAllow58 { get; set; }
        public bool? IsAllow59 { get; set; }
        public bool? IsAllow60 { get; set; }
        public bool? IsAllow61 { get; set; }
        public bool? IsAllow62 { get; set; }
        public bool? IsAllow63 { get; set; }
        public bool? IsAllow64 { get; set; }
        public bool? IsAllow65 { get; set; }

        public virtual JsTblSubCategory? SubCategory { get; set; }
        public virtual ICollection<JsTblMockup> JsTblMockups { get; set; }
        public virtual ICollection<JsTblOrderRequest> JsTblOrderRequests { get; set; }
        public virtual ICollection<JsTblPriceList> JsTblPriceLists { get; set; }
        public virtual ICollection<JsTblProductSizePriceMaster> JsTblProductSizePriceMasters { get; set; }
    }
}
