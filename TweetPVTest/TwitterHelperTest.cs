using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetPV;

namespace TweetPVTest
{
    [TestClass]
    public class TwitterHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TwitterHelper.UpdateStatusAsync("テスト " + DateTime.Now.ToString("hh:mm:ss")).Wait();
        }
    }
}
