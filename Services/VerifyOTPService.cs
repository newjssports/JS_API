using Microsoft.IdentityModel.Tokens;
using SportsOrderApp.Core;
using SportsOrderApp.DTOs;
using SportsOrderApp.Helper;
using SportsOrderApp.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IVerifyOTPService
    {
        Task<VerificationCodeResult> VerifyMobileOTP(long userId, string otp);
        Task<bool> VerifyEmailOTP(long userId, string otp);
    }
    public class VerifyOTPService : IVerifyOTPService
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IUserRepository _userRepository;
        //private readonly JwtSettings _jwtSettings;

        public VerifyOTPService(IVerificationCodeRepository verificationCodeRepository, IUserRepository userRepository)
        {
            _verificationCodeRepository = verificationCodeRepository;
            _userRepository = userRepository;
            //_jwtSettings = jwtSettings;
        }

        public async Task<VerificationCodeResult> VerifyMobileOTP(long userId, string otp)
        {
            var res = await VerifyOTP(userId, otp, "Mobile");
            return res;

        }

        public async Task<bool> VerifyEmailOTP(long userId, string otp)
        {
            var res = await VerifyOTP(userId, otp, "Email");
            if (res.Flag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<VerificationCodeResult> VerifyOTP(long userId, string code, string type)
        {
            var mobileOTP = _verificationCodeRepository.
                                        FindBy(v => v.UserId == userId && v.Code == code && v.CodeType == type && !v.IsUsed).
                                        FirstOrDefault();
            var user = _userRepository
                .FindBy(v => v.UserId == userId && v.IsVerified == false).FirstOrDefault();

            //var existUser = await _context.Users
            //    .FirstOrDefaultAsync(v => v.Id == userId);

            if (mobileOTP == null || mobileOTP.Expiry < DateTime.UtcNow)
            {
                var res = new VerificationCodeResult
                {
                    Message = "Invalid OTP or OTP Expired!",
                    Code = "",
                    Flag = false
                };
                user.IsVerified = false;
                _userRepository.Update(user);
                _userRepository.Commit();
                return res;

            }
            else
            {
                UserTokenModel userTokenData = new UserTokenModel();
                userTokenData.UserId = user.UserId;
                userTokenData.UserName = user.UserName;
                userTokenData.IcNumber = user.IcNumber;
                userTokenData.Mobile = user.Mobile;
                userTokenData.Email  = user.Email;
                userTokenData.UserType = user.UserType;

                //var token = GenerateJwtToken(userTokenData);
                var token = GenerateToken(userTokenData);
                var res = new VerificationCodeResult
                {
                    Message = "Mobile OTP Success!",
                    Code = "",
                    Flag = true,
                    Token = token
                };
                user.IsVerified = true;
                mobileOTP.IsUsed  = true;
                _verificationCodeRepository.Update(mobileOTP);
                _verificationCodeRepository.Commit();
                _userRepository.Update(user);
                _userRepository.Commit();
                return res;
            }

            //mobileOTP.IsUsed = true;
            //await _context.SaveChangesAsync();
            //if (type == "Mobile")
            //{
            //    string emailOtp = GenerateOTP();
            //    _context.VerificationCodes.Add(new VerificationCode
            //    {
            //        UserId = userId,
            //        Code = emailOtp,
            //        CodeType = "Email",
            //        Expiry = DateTime.UtcNow.AddMinutes(5),
            //        IsUsed = false
            //    });
            //    var email = "";
            //    if (existUser.Status == "Login")
            //    {
            //        email = MaskingHelper.MaskEmail(existUser.Email);
            //    }
            //    else
            //    {
            //        email = existUser.Email;
            //    }

            //    await _context.SaveChangesAsync();
            //    var res = new VerificationCodeResult
            //    {
            //        Message = "Mobile OTP Success. OTP sent to Email. " + email + " ",
            //        Code = emailOtp,
            //        Flag = true
            //    };
            //    return res;
            //}
            //else
            //{
            //    if (otp.IsUsed)
            //    {
            //        user.IsVerified = true;
            //        await _context.SaveChangesAsync();
            //    }
            //}
            //var res2 = new VerificationCodeResult
            //{
            //    Message = "",
            //    Code = "",
            //    Flag = true
            //};
            //return res2;
        }

        //public string GenerateJwtToken(UserTokenModel user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret); // Ensure this key is at least 32 characters
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) }),
        //        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public string GenerateJwtToken(UserTokenModel user)
        {
            var jwtSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("JwtSettings");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]); // Ensure the key is at least 32 characters long for better security

            // Add claims to include user details in the token
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("ICNumber", user.IcNumber),
                new Claim("Email", user.Email),
                new Claim("MobileNumber", user.Mobile),
                new Claim("UserType", user.UserType),
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateToken(UserTokenModel user)
        {
            var jwtSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("JwtSettings");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim("userId", user.UserId.ToString()),
        new Claim("TokenType", "FullAccess"), // Custom claim for full access
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token valid for 2 hours
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
