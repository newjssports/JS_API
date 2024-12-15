using SportsOrderApp.Entities;

namespace SportsOrderApp.Models
{
    public class User
    {
        public long UserId { get; set; }
        public long? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string Mobile { get; set; } = null!;
        public string? Mobile2 { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? IdCardNo { get; set; }
        public string? Nationality { get; set; }
        public string? Sso { get; set; }
        public string? ApiKey { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public string? UserType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? EmpId { get; set; }
        public int? Pin { get; set; }
        public bool? IsPinConfirmed { get; set; }
        public bool? IsVerified { get; set; }
        public bool? PrivacyPolicyAccepted { get; set; }
        public string IcNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual JsTblRole? Role { get; set; }
        public virtual ICollection<JsTblMockupDesignStepUserRight> JsTblMockupDesignStepUserRights { get; set; }
        public virtual ICollection<JsTblMockupLog> JsTblMockupLogClientUsers { get; set; }
        public virtual ICollection<JsTblMockupLog> JsTblMockupLogHostUsers { get; set; }
        public virtual ICollection<JsTblMockup> JsTblMockups { get; set; }
        public virtual ICollection<JsTblOrderDesignStepUserRight> JsTblOrderDesignStepUserRights { get; set; }
        public virtual ICollection<JsTblOrderRequestLog> JsTblOrderRequestLogs { get; set; }
        public virtual ICollection<JsTblOrderRequest> JsTblOrderRequests { get; set; }
        public virtual ICollection<JsTblPriceList> JsTblPriceLists { get; set; }
        public virtual ICollection<JsTblProductSizePriceMaster> JsTblProductSizePriceMasters { get; set; }
        public virtual ICollection<JsTblVerificationCode> JsTblVerificationCodes { get; set; }
    }

    public class UserProfile
    {
        public long UserId { get; set; }
        public long? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string Mobile { get; set; } = null!;
        public string? Mobile2 { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? IdCardNo { get; set; }
        public string? Nationality { get; set; }
        public string? Sso { get; set; }
        public string? ApiKey { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public string? UserType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? EmpId { get; set; }
        public int? Pin { get; set; }
        public bool? IsPinConfirmed { get; set; }
        public bool? IsVerified { get; set; }
        public bool? PrivacyPolicyAccepted { get; set; }
        public string IcNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual JsTblRole? Role { get; set; }
        public virtual ICollection<JsTblMockupDesignStepUserRight> JsTblMockupDesignStepUserRights { get; set; }
        public virtual ICollection<JsTblMockupLog> JsTblMockupLogClientUsers { get; set; }
        public virtual ICollection<JsTblMockupLog> JsTblMockupLogHostUsers { get; set; }
        public virtual ICollection<JsTblMockup> JsTblMockups { get; set; }
        public virtual ICollection<JsTblOrderDesignStepUserRight> JsTblOrderDesignStepUserRights { get; set; }
        public virtual ICollection<JsTblOrderRequestLog> JsTblOrderRequestLogs { get; set; }
        public virtual ICollection<JsTblOrderRequest> JsTblOrderRequests { get; set; }
        public virtual ICollection<JsTblPriceList> JsTblPriceLists { get; set; }
        public virtual ICollection<JsTblProductSizePriceMaster> JsTblProductSizePriceMasters { get; set; }
        public virtual ICollection<JsTblVerificationCode> JsTblVerificationCodes { get; set; }
    }
}
