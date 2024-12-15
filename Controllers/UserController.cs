using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        public UserController(IUserService userService, IUserProfileService userProfileService) 
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }


        #region GET
        [HttpGet("clientList")]
        public IActionResult GetClientUserList()
        {
            var client = _userService.GetClientUserList();
            return Ok(client);
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userProfile = await _userProfileService.GetUserProfileAsync(Request.Headers);
                return Ok(userProfile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }


}
