using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.DTOs;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize(Policy = "OTPOnly")] // Only accessible with the first token
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyOTPController : ControllerBase
    {
        private readonly IVerifyOTPService _verifyOTPService;

        public VerifyOTPController(IVerifyOTPService verifyOTPService)
        {
            _verifyOTPService = verifyOTPService;
        }


        [HttpPost("verify-mobile-otp")]
        public async Task<IActionResult> VerifyMobileOTP([FromBody] VerifyOTPRequest request)
        {
            var userId = Convert.ToInt64(User.FindFirst("userId").Value);
            var res = await _verifyOTPService.VerifyMobileOTP(userId, request.Code);
            if (res.Flag == false)
                return BadRequest("Invalid or expired OTP.");
            return Ok(new { Message = res.Message , Token = res.Token
               // , OTP = res.Code
            });
        }

        [HttpPost("verify-email-otp")]
        public async Task<IActionResult> VerifyEmailOTP([FromBody] VerifyOTPRequest request)
        {
            var userId = Convert.ToInt64(User.FindFirst("userId").Value);
            var isValid = await _verifyOTPService.VerifyEmailOTP(userId, request.Code);
            if (!isValid)
                return BadRequest("Invalid or expired OTP.");
            return Ok("Email OTP verified | Set 6 digit PIN.");
        }
    }
}
