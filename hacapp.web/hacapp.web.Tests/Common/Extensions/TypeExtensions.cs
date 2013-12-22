using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace hacapp.web.Tests.Common.Extensions
{
    static class TypeExtensions
    {
        internal static bool AllowsOnlyAuthorizedAccessToMethod(this Type type, string methodToTest)
        {
            bool allowsAuthorizedAccessOnly;
            var classAttributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            var methodInfo = type.GetMethod(methodToTest);
            if (classAttributes.Any())
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
                allowsAuthorizedAccessOnly = !attributes.Any();
            }
            else
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true);
                allowsAuthorizedAccessOnly = attributes.Any();
            }
            return allowsAuthorizedAccessOnly;
        }
    }
}
