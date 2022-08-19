using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class TweetTests
    {
        Tweet tweet = new Tweet();

        #region GenerateTweet
        [TestMethod]
        public void GenerateTweetTest()
        {
            string msgHeader = "T123456789";
            string msgType = "Tweet";
            string msgBody = "This is a test";
            string msgSender = "@Liam";
            Tweet eResult = new Tweet("T123456789", "Tweet", "This is a test", "@Liam");

            Tweet aResult = new Tweet(msgHeader, msgType, msgBody, msgSender);

            Assert.AreEqual(eResult.Header, aResult.Header);
            Assert.AreEqual(eResult.Type, aResult.Type);
            Assert.AreEqual(eResult.Body, aResult.Body);
            Assert.AreEqual(eResult.Sender, aResult.Sender);
        }
        #endregion

        #region ValidateSubject
        [TestMethod]
        public void ValidateTweeterTest()
        {
            string tweeter = "@Liam";
            bool aResult = tweet.ValidateTweeter(tweeter);

            Assert.IsTrue(aResult);
        }

        [TestMethod]
        public void ValidateTweeterLimitTest()
        {
            string tweeter = "@eeeeeeeeeeeeeeee";
            bool aResult = tweet.ValidateTweeter(tweeter);

            Assert.IsFalse(aResult);
        }

        [TestMethod]
        public void ValidateTweeterExceptionalTest()
        {
            string tweeter = "Test";
            bool aResult = tweet.ValidateTweeter(tweeter);

            Assert.IsFalse(aResult);
        }

        [TestMethod]
        public void ValidateTweeterNullTest()
        {
            string tweeter = "";

            Assert.ThrowsException<ArgumentNullException>(() => tweet.ValidateTweeter(tweeter));
        }
        #endregion

    }
}