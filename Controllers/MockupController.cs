using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsOrderApp.DTOs;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MockupController : ControllerBase
    {
        private readonly IMockupService _mockupService;
        public MockupController(IMockupService mockupService) 
        {
            _mockupService = mockupService;
        }

        #region POST
        [HttpPost("addNewMockup")]
        public IActionResult AddNewMockup([FromBody] List<AddNewMockup> addNewMockups)
        {
            if (addNewMockups == null || !addNewMockups.Any())
            {
                return BadRequest("Invalid mockup data. The array is empty or null.");
            }

            try
            {
                // Access the custom header (for example, 'Authorization' or any custom header)
                if (Request.Headers.TryGetValue("Authorization", out var headerValue))
                {
                    // Process the header value
                    Console.WriteLine("Custom Header Value: " + headerValue);
                }
                else
                {
                    return BadRequest("Required header not found.");
                }
                var max_val = _mockupService.GenerateNewMaxValue("JS_TBL_MOCKUP", "MOCKUP_REQUEST_NO").Result;
                foreach (var mockup in addNewMockups)
                {
                    _mockupService.AddNewMockup(mockup, max_val);
                }
                
                return Ok("All mockups submitted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (if necessary)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file,long? mockupId)
        {
            try
            {
                var imagePath = await _mockupService.SaveImageAsync(file, 1);
                return Ok(new { imagePath });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET
        [HttpGet("getMockupsClientRequest")]
        public IActionResult GetMockupsClientRequest()
        {
            var mockups = _mockupService.GetMockupsClientRequest();
            return Ok(mockups);
        }

        [HttpGet("getMockupsMoveToDesignDept")]
        public IActionResult GetMockupsMoveToDesigningDepartment()
        {
            var mockups = _mockupService.MoveToDesigningDepartment();
            return Ok(mockups);
        }

        [HttpGet("getMockupsAssignToDesigner")]
        public IActionResult GetMockupsAssignToDesigner()
        {
            var mockups = _mockupService.getMockupsAssignToDesigner();
            return Ok(mockups);
        }

        [HttpGet("getApprovedMockups")]
        public IActionResult GetApprovedMockups()
        {
            var mockups = _mockupService.GetApprovedMockups(2);
            return Ok(mockups);
        }
        #endregion
    }
}
