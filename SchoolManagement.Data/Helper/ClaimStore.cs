namespace SchoolManagement.Data.Helper;
using System.Security.Claims;

public static class ClaimStore
{
    public static List<Claim> claims = new()
    {
        new Claim("Create Student","false"),
        new Claim("Edit Student","false"),
        new Claim("Delete Student","false"),
    };
}
