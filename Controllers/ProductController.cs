using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.DTOs;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService ;
        private readonly IProductPriceListService _productPriceListService;
        public ProductController(IMapper mapper, IProductService productService, IProductPriceListService productPriceListService = null)
        {
            _mapper = mapper;
            _productService = productService;
            _productPriceListService = productPriceListService;
        }
        #region
        [HttpGet("getProductBySubCateId")]
        public IActionResult GetProductBySubCateId(long id)
        {
            var mainCate = _productService.GetProductBySubCateId(id);
            return Ok(mainCate);
        }
        #endregion

        #region
        [HttpPost("getProductPriceListByUser")]
        public IActionResult GetProductPriceListByUser([FromBody] GetProductPriceByUser model)
        {
            var res = _productPriceListService.GetProductPriceListByUser(model);
            return Ok(res);
        }

        #endregion

        #region

        #endregion
    }
}
