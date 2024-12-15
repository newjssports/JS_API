namespace SportsOrderApp.DTOs
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ICLoginModel
    {
        public string IcNumber { get; set; }
    }

    public class RegisterModel
    {
        public long UserId { get; set; }
        public long? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        public string? UserType { get; set; }
        public string Email { get; set; }
    }

    public class UserTokenModel
    {
        public long UserId { get; set; }
        public string IcNumber { get; set; }
        public long? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        public string? UserType { get; set; }
        public string Email { get; set; }
        public bool? IsVerified { get; set; }
        public string Mobile { get; set; } = null!;
    }

    public class VerificationCodeResult
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public bool Flag { get; set; }
        public string Token { get; set; }
    }

    public class VerifyOTPRequest
    {
       // public long USER_ID { get; set; }
        public string Code { get; set; }
    }
}
