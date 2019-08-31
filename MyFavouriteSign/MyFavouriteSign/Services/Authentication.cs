using Microsoft.AppCenter.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MyFavouriteSign.Services
{
    public static class Authentication
    {
        public static UserInformation userInfo;
        public static string DisplayName;
        public static async Task SignInAsync()
        {
            try
            {
                // Sign-in succeeded, UserInformation is not null.
                userInfo = await Auth.SignInAsync();

                // Get tokens. They are not null.
                string idToken = userInfo.IdToken;
                string accessToken = userInfo.AccessToken;

                GetInfo(userInfo);
            }
            catch (Exception e)
            {
                // Do something with sign-in failure.
            }
        }

        public static void SignOut()
        {
            Auth.SignOut();
        }

        static void GetInfo(UserInformation userInfo)
        {
            // Decode the raw token string to read the claims.
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwToken = tokenHandler.ReadJwtToken(userInfo.IdToken);

                // Get display name.
                var displayName = jwToken.Claims.FirstOrDefault(t => t.Type == "name")?.Value;
                if (displayName != null)
                {
                    DisplayName = displayName;
                }
            }
            catch (ArgumentException)
            {
                // Handle error.
            }
        }
    }
}
