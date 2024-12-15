using SportsOrderApp.Models;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IUserProfileService
    {
        Task<UserProfile> GetUserProfileAsync(IHeaderDictionary headers);
    }
    public class UserProfileService: IUserProfileService
    {
        private readonly IUserRepository _userRepository;

        public UserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserProfile> GetUserProfileAsync(IHeaderDictionary headers)
        {
            if (!headers.TryGetValue("Authorization", out var authorizationHeader))
                throw new ArgumentException("Authorization header is missing");

            if (!headers.TryGetValue("UserId", out var userIdHeader))
                throw new ArgumentException("UserId header is missing");

            if (!long.TryParse(userIdHeader.ToString(), out var userId))
                throw new ArgumentException("Invalid UserId header value");

            long USER_ID = Convert.ToInt64(userIdHeader.ToString());
            // Verify the Authorization token (if necessary)
            var token = authorizationHeader.ToString();
            if (!IsValidToken(token)) // Add token validation logic here
                throw new UnauthorizedAccessException("Invalid Authorization token");

            // Retrieve user profile from the database
            var userProfile = await _userRepository.FindByAsync(x => x.UserId == USER_ID);
            if (userProfile == null)
                throw new UnauthorizedAccessException("User not found");

            return (UserProfile)userProfile;
        }

        private bool IsValidToken(string token)
        {
            // Implement token validation logic (e.g., JWT verification)
            return !string.IsNullOrEmpty(token);
        }
    }
}
