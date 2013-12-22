using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using hacapp.web.Tests.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hacapp.web;
using hacapp.web.Controllers;

namespace hacapp.web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestClass]
        public class TheIndexMethod
        {
            [TestMethod]
            public void ShouldBeDecoratedWithTheAuthorizeAttribute()
            {
                bool allowsAuthorizedAccessOnly = false;
                allowsAuthorizedAccessOnly = typeof(HomeController).AllowsOnlyAuthorizedAccessToMethod("Index");
                Assert.IsTrue(allowsAuthorizedAccessOnly, "No AuthorizeAttribute restriction found on Index() method");
            }            
        }

        [TestClass]
        public class TheAboutMethod : HomeControllerTest
        {
            [TestMethod]
            public void ShouldAllowAnonymousAccess()
            {
                Assert.IsFalse(typeof(HomeController).AllowsOnlyAuthorizedAccessToMethod("About"), "No anonymous access to the about method allowed");
            }
        }
    }
}
