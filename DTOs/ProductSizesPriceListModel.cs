using SportsOrderApp.Entities;

namespace SportsOrderApp.DTOs
{
    public class ProductSizesPriceListModel
    {
        public long SizePriceId { get; set; }
        public long? SizePriceHeaderId { get; set; }
        public long? ProductSizeId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Discount { get; set; }
    }

    public class ProductSizePriceListDataModel
    {
        public long SizePriceHeaderId { get; set; }
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public bool? Active { get; set; }
        //public bool? Deleted { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
     //   public virtual ProductModel? Product { get; set; }
      //  public virtual UserListModel? User { get; set; }
      //  public virtual List<ProductSizesPriceListModel> JsTblProductSizePriceDetails { get; set; }
    }

    public class SizePriceIdsRequest
    {
        public long[] prodSizeIds { get; set; }
        public long? productId { get; set; }
    }
}
