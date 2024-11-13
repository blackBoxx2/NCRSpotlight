using System.Security.Claims;

namespace NCRSPOTLIGHT
{
    public class ClaimStore
    {
        public static List<Claim> claimList =
            [
                new Claim("Create", "Create"),
                new Claim("Edit", "Edit"),
                new Claim("Delete", "Delete")
            ];
    }
}
