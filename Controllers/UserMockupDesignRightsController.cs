using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.Core;
using SportsOrderApp.DTOs;
using SportsOrderApp.Models;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize(Policy = "FullAccess")] // Requires full-access token
    [Route("api/[controller]")]
    [ApiController]
    public class UserMockupDesignRightsController : ApiControllerExtensions
    {
        private readonly IUserMockupDesignRightsService _userMockupDesignRightsService;
        private readonly IUserProfileService _userProfileService;
        public UserMockupDesignRightsController(IUserMockupDesignRightsService userMockupDesignRightsService, IUserProfileService userProfileService)
        {
            _userMockupDesignRightsService = userMockupDesignRightsService;
            _userProfileService = userProfileService;
        }
        #region GET
        [HttpGet("userMockupDesignActionRights")]
        public IActionResult GetUerMockupDesignActionRights()
        {
            
            try
            {
                //var userProfile =  _userProfileService.GetUserProfileAsync(Request.Headers);
                //long userId = userProfile.Result.UserId;
                var user_Id = User.FindFirst("UserId")?.Value; // Retrieve UserId from claims
                long userId = Convert.ToInt64(user_Id);
                if (string.IsNullOrEmpty(user_Id))
                {
                    return Unauthorized("UserId is missing in the token.");
                }
                var userRight = _userMockupDesignRightsService.GetUerMockupDesignActionRights(userId);
                return Ok(userRight);

                //return Ok(userProfile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            //// Access the custom header (for example, 'Authorization' or any custom header)
            //if (Request.Headers.TryGetValue("Authorization", out var headerValue))
            //{
            //    // Process the header value
            //    Console.WriteLine("Custom Header Value: " + headerValue);
            //}
            //else
            //{
            //    return BadRequest("Required header not found.");
            //}
            //if (Request.Headers.TryGetValue("UserId", out var user_id))
            //{
            //    // Process the header value
            //    userId = Convert.ToInt64(user_id.ToString());
            //    Console.WriteLine("Custom Header Value: " + user_id);
            //}
            //else
            //{
            //    return BadRequest("Required header not found.");
            //}
            
        }
        [HttpGet("getMockupDesignNames")]
        public IActionResult GetMockupDesignNames()
        {
            var name = _userMockupDesignRightsService.GetMockupDesignNames();
            return Ok(name);
        }
        [HttpGet("getMockupDesignedNames")]
        public IActionResult GetMockupDesignedNames()
        {
            var name = _userMockupDesignRightsService.GetMockupDesignedNames();
            return Ok(name);
        }
        [HttpGet("getMockupDesignSteps")]
        public IActionResult GetMockupDesignSteps()
        {
            var step = _userMockupDesignRightsService.GetMockupDesignSteps();
            return Ok(step);
        }
        #endregion

        #region POST
        [HttpPost("createMockupStep")]
        public IActionResult CreateMockupStep([FromBody] AddMockupDesignStepsModel model)
        {

             var res = _userMockupDesignRightsService.CreateMockupStep(model);
            if (res)
            {
                return Ok("Mockup Step Added Successfully");
            }
            else
            {
                return Ok("Mockup Step Already Added");
            }
            
        }
        [HttpPost("createMockupDesigningStep")]
        public IActionResult CreateMockupDesigningStep([FromBody] AddMockupDesignStepsModel model)
        {

            var res = _userMockupDesignRightsService.CreateMockupDesigningStep(model);
            if (res)
            {
                return Ok("Mockup Step Added Successfully");
            }
            else
            {
                return Ok("Mockup Step Already Added");
            }

        }
        #endregion
    }
}
