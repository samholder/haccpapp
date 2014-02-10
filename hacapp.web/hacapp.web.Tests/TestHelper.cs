using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace hacapp.web.Tests
{
    public class TestHelper
    {
        public static GenericPrincipal GetPrincipal()
        {
            const string username = "username";
            Guid userid = Guid.NewGuid();

            return GetPrincipal(userid.ToString(), username);
        }

        public static GenericPrincipal GetPrincipal(string userId, string userName)
        {
            var claims = new List<Claim>
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userName),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId)
            };
            var genericIdentity = new GenericIdentity(userName);
            genericIdentity.AddClaims(claims);
            var genericPrincipal = new GenericPrincipal(genericIdentity, new string[0]);
            return genericPrincipal;
        }
    }
}