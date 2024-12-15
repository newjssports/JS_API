using AutoMapper;
using SportsOrderApp.DTOs;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IProductSizeListService
    {
        public IList<ProductSizeListModel> GetProductSizes();
        public IList<ProductSizesPriceListModel> GetProductSizesPriceList(SizePriceIdsRequest sizePriceIdsRequest);
    }
    public class ProductSizeListService: IProductSizeListService
    {
        private readonly IMapper _mapper;
        private readonly IProductSizeListRepository _productSizeListRepository;
        private readonly IProductSizePriceDetailRepository _productSizePriceDetailRepository;
        private readonly IProductSizePriceMasterRepository _productSizePriceMasterRepository;
        public ProductSizeListService(
            IMapper mapper,
            IProductSizeListRepository productSizeListRepository,
            IProductSizePriceDetailRepository productSizePriceDetailRepository,
            IProductSizePriceMasterRepository productSizePriceMasterRepository
            ) 
        {
            _mapper = mapper;
            _productSizeListRepository = productSizeListRepository;
            _productSizePriceDetailRepository = productSizePriceDetailRepository;
            _productSizePriceMasterRepository = productSizePriceMasterRepository;
        }
        public IList<ProductSizeListModel> GetProductSizes()
        {
            var res = _productSizeListRepository.GetAll().ToList();
            return _mapper.Map<List<ProductSizeListModel>>( res );
        }

        public IList<ProductSizesPriceListModel> GetProductSizesPriceList(SizePriceIdsRequest sizePriceIdsRequest)
        {
            List<ProductSizesPriceListModel> list = new List<ProductSizesPriceListModel>();

            long headerId = _productSizePriceMasterRepository.FindBy(a => a.ProductId == sizePriceIdsRequest.productId && 
                                                                    a.UserId == 2 && a.Active == true && a.Deleted == false)
                                                                    .Select(x => x.SizePriceHeaderId).FirstOrDefault();

            if (headerId != 0)
            {
                foreach (var id in sizePriceIdsRequest.prodSizeIds)
                {
                    var rst = _productSizePriceDetailRepository.FindBy(a => a.SizePriceHeaderId == headerId &&
                                                                        a.ProductSizeId == id && a.Active == true).FirstOrDefault();

                    var res = _mapper.Map<ProductSizesPriceListModel>(rst);

                    list.Add(res);
            }

            }

           


            return list;
        }
    }
}
