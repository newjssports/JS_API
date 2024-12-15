using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize(Policy = "FullAccess")] // Requires full-access token
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemCategoryService _itemCategoryService;
        public ItemCategoryController(IMapper mapper, IItemCategoryService itemCategoryService) {
            _mapper = mapper;
            _itemCategoryService = itemCategoryService;
        }
        #region

        #endregion

        #region

        #endregion

        #region

        #endregion
    }
}
