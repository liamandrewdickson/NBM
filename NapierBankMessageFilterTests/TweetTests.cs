using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class TweetTests
    {
        Tweet tweet = new Tweet();

        #region ValidateSubject
        [TestMethod]
        public void ValidateTweeterNullTest()
        {
            string tweeter = "";

            Assert.ThrowsException<ArgumentNullException>(() => tweet.ValidateTweeter(tweeter));
        }
        #endregion
    }
}