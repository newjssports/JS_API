namespace SportsOrderApp.Helper
{
    public static class MaskingHelper
    {
        public static string MaskMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile) || mobile.Length < 4)
                return mobile; // Return as is if invalid length
            return new string('*', mobile.Length - 3) + mobile.Substring(mobile.Length - 3);
        }

        public static string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                return email; // Return as is if invalid format

            var atIndex = email.IndexOf('@');
            var namePart = email.Substring(0, atIndex);
            var domainPart = email.Substring(atIndex);

            if (namePart.Length > 2)
            {
                namePart = namePart.Substring(0, 2) + new string('*', namePart.Length - 2);
            }
            else
            {
                namePart = new string('*', namePart.Length);
            }

            var domainParts = domainPart.Split('.');
            if (domainParts.Length > 1)
            {
                domainPart = "@" + new string('*', domainParts[0].Length - 1) + domainParts[0].Last() + "." + domainParts[1];
            }

            return namePart + domainPart;
        }

        public static string GenerateOTP()
        {
            return new Random().Next(1000, 9999).ToString();
        }
    }
}
