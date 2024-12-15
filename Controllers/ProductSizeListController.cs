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
    public class ProductSizeListController : ControllerBase
    {
        private readonly IProductSizeListService _productSizeListService;
        public ProductSizeListController(IProductSizeListService productSizeListService) 
        {
            _productSizeListService = productSizeListService;
        }

        #region Get
        [HttpGet("getProductSizes")]
        public IActionResult GetProductSizes()
        {
            var fab = _productSizeListService.GetProductSizes();
            return Ok(fab);
        }
        #endregion

        [HttpPost("getProductSizesPriceList")]
        public IActionResult GetProductSizesPriceList([FromBody] SizePriceIdsRequest model)
        {
            var res = _productSizeListService.GetProductSizesPriceList(model);
            return Ok(res);
        }
    }
}
