using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SportsOrderApp.Core;
using SportsOrderApp.DTOs;
using SportsOrderApp.Entities;
using SportsOrderApp.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IUserService
    {
        public IList<UserListModel> GetClientUserList();
        public bool ValidateUser(string username, string password);
        public VerificationCodeResult ValidateICUser(string icNumber);
        UserModel GetUserByUsername(string username);
        public void RegisterUser(RegisterModel model);
    }
    public class UserService: IUserService
    {
       private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        public UserService(IMapper mapper, IUserRepository userRepository,
            IVerificationCodeRepository verificationCodeRepository) 
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _verificationCodeRepository = verificationCodeRepository;
        }
        public UserModel GetUserByUsername(string username)
        {
            var user = _userRepository.FindBy(x => x.UserName == username).FirstOrDefault();
            return _mapper.Map<UserModel>(user);
        }
        public IList<UserListModel> GetClientUserList()
        {
            var clinet = _userRepository.FindBy(x => x.Active == true
                                                && x.Deleted == false
                                                && x.UserType == "CLIENT").ToList();
            return _mapper.Map<List<UserListModel>>(clinet);

        }
        public bool ValidateUser(string username, string password)
        {
            var user = GetUserByUsername(username);

            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
        public VerificationCodeResult ValidateICUser(string icNumber)
        {
            var user = _userRepository.FindBy(x => x.IcNumber == icNumber).FirstOrDefault();
            VerificationCodeResult res = new VerificationCodeResult();

            if (user == null)
            {
                res.Flag = false;
                return res;
            }
            user.IsVerified = false;
            _userRepository.Update(user);
            _userRepository.Commit();

            string otp = MaskingHelper.GenerateOTP();
            var verification = new JsTblVerificationCode
            {
                UserId = user.UserId,
                Code = otp,
                CodeType = "Mobile",
                Expiry = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };
            _verificationCodeRepository.Add(verification);
            _verificationCodeRepository.Commit();

            string maskedMobile = MaskingHelper.MaskMobile(user.Mobile);
            var token = GenerateToken(user.UserId);
            res.Code = otp;
            res.Message = "For Account Verification OTP Sent To Your Mobile No. "+ maskedMobile +" ";
            res.Flag = true;
            res.Token = token;
            return res;
        }

        public void RegisterUser(RegisterModel model)
        {
            var user = new RegisterModel
            {
                UserName = model.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                UserType = model.UserType,
                RoleId = model.RoleId,
                
            };
            var res = _mapper.Map<JsTblUser>(user);
            res.Active = true;
            res.Deleted = false;
            //res.RoleId = 1;
            res.CreatedBy = "AAMIR";
            res.CreatedDate = DateTime.Now;
            res.ModifiedBy = "AAMIR";
            res.ModifiedDate = DateTime.Now;
            _userRepository.Add(res);
            _userRepository.Commit();
        }

        public string GenerateToken(long userId)
        {
            var jwtSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("JwtSettings");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim("userId", userId.ToString()),
        new Claim("TokenType", "OTPVerification"), // Custom claim for token type
        //new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
