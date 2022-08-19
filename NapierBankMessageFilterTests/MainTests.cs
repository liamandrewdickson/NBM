using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class MainTests
    {
        Main main = new Main();

        #region ValidateMessageType
        [TestMethod]
        public void ValidateMessageTypeTest()
        {
            string msgID = "E123456789";
            string eResult = "Email";
            string aResult = main.ValidateMessageType(msgID);

            Assert.AreEqual(eResult, aResult);
        }

        [TestMethod]
        public void ValidateMessageTypeNullTest()
        {
            string msgID = "";

            Assert.ThrowsException<ArgumentNullException>(() => main.ValidateMessageType(msgID));
        }
        #endregion


        #region ValidateMessageLimitTest
        [TestMethod]
        public void ValidateMessageLimitTest()
        {
            string msgSender = "liam.dickson@liam.co.uk";
            string[] msgType = { "Email","Tweet","SMS" };
            string msgBody = "This is a Test";
            foreach (string t in msgType)
            {
                switch (t) 
                {
                    case "Tweet":
                        msgSender = "@Liam";
                        break;
                    case "SMS":
                        msgSender = "07884969094";
                        break;
                }

                bool aResult = main.ValidateMessageLimit(t, msgSender, msgBody);
                Assert.IsTrue(aResult);
            }
           
        }

        [TestMethod]
        public void ValidateLimitTest()
        {
            string msgSender = "liam.dickson@liam.co.uk";
            string[] msgType = { "Email", "Tweet", "SMS" };
            string msgBody = "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            int p;
            foreach (string t in msgType)
            {
                switch (t)
                {
                    case "Tweet":
                        msgSender = "@Liam";
                        break;
                    case "SMS":
                        msgSender = "07884969094";
                        break;
                }
          
                bool aResult = main.ValidateMessageLimit(t, msgSender, msgBody);
                Assert.IsFalse(aResult);
            }

        }

        [TestMethod]
        public void ValidateMessageLimitNullTest()
        {
            string msgType = "Email";
            string msgSender = "liam.dickson@liam.co.uk";
            string msgBody = "";

            Assert.ThrowsException<ArgumentNullException>(() => main.ValidateMessageLimit(msgType, msgSender, msgBody));
        }
        #endregion
    }
}