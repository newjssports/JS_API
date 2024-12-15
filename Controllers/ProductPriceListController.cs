using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.DTOs;
using SportsOrderApp.Models;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceListController : ControllerBase
    {
        private readonly IProductPriceListService _productPriceListService;
        ProductPriceListController(IProductPriceListService productPriceListService)
        {
            _productPriceListService   = productPriceListService;
        }
        #region POST
        [HttpPost("getProductPriceListByUser")]
        public IActionResult GetProductPriceListByUser([FromBody] GetProductPriceByUser model)
        {
            var res = _productPriceListService.GetProductPriceListByUser(model);
            return Ok(res);
        }
        #endregion
    }
}
