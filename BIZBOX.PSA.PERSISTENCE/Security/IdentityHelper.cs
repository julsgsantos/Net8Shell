using BIZBOX.PSA.DOMAIN.Enums.Accounts;
using System.Security.Claims;

namespace BIZBOX.PSA.PERSISTENCE.Security
{
    public class IdentityHelper
    {
        public static IdentityType IdentityType
        {
            get
            {
                var principal = Thread.CurrentPrincipal;

                if (principal == null)
                    return IdentityType.None;

                if (Email != null)
                    return IdentityType.User;

                return IdentityType.None;
            }
        }


        #region User/App
        private static IEnumerable<Claim> Claims
        {
            get
            {
                var identity = Thread.CurrentPrincipal?.Identity as ClaimsIdentity;
                if (identity?.Claims == null)
                {
                    return Enumerable.Empty<Claim>();
                }
                return identity.Claims;
            }
        }


        public static string UserId
        {
            get
            {
                var claim = Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserId);
                return claim?.Value;
            }
        }

        public static string Email
        {
            get
            {
                var claim = Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Email);
                return claim?.Value;
            }
        }

        public static List<string> Role
        {
            get
            {
                var roles = new List<string>();
                var claim = Claims.Where(x => x.Type == CustomClaimTypes.Role).ToList();
                claim.ForEach(x => roles.Add(x.Value));
                return roles;
            }
        }
        #endregion
    }
}
