using AutoMapper;
using SportsOrderApp.DTOs;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IProductPriceListService
    {
        ProductPriceListModel GetProductPriceListByUser(GetProductPriceByUser getProductPriceByUser);
    }
    public class ProductPriceListService : IProductPriceListService
    {
        private readonly IMapper _mapper;
        private readonly IProductPriceListRepository _productPriceListRepository;
        public ProductPriceListService(IMapper mapper, IProductPriceListRepository productPriceListRepository) 
        {
            _mapper = mapper;
            _productPriceListRepository = productPriceListRepository;
        }

        public ProductPriceListModel GetProductPriceListByUser(GetProductPriceByUser getProductPriceByUser)
        {
            var res = _productPriceListRepository.FindBy(x => x.ProductId == getProductPriceByUser.ProductId &&
                                                         x.UserId == getProductPriceByUser.UserId &&
                                                         x.Active == true &&
                                                         x.Deleted == false).FirstOrDefault();

            var result = _mapper.Map<ProductPriceListModel>(res);
            return result;
        }
    }
}
