using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetPV;

namespace TweetPVTest
{
    [TestClass]
    public class AnalyticsHelperTest
    {
        [TestMethod]
        [Timeout(60000)]
        public void TestMethod1()
        {
            var file = ConfigurationManager.AppSettings["analyticsKeyFile"];
            var sut = new AnalyticsHelper(file);
            var pv = sut.GetPvAsync().Result;

            Assert.IsTrue(pv > 0);
        }
    }
}
