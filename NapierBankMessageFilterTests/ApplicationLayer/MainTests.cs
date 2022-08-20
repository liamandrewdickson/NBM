using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class MainTests
    {
        Main main;

        [TestInitialize]
        public void TestInitialize()
        {
            main = new Main();
            main.NBMStart();
        }

        #region ValidateMessageType
        [TestMethod]
        public void ValidateMessageTypeTest()
        {
            string msgHeader = "E123456789";
            string[] eResult = { "Email", "Tweet", "SMS", "" };
            foreach (string e in eResult)
            {
                switch (e)
                {
                    case "Tweet":
                        msgHeader = "T123456789";
                        break;
                    case "SMS":
                        msgHeader = "S123456789";
                        break;
                    case "":
                        msgHeader = "P123456789";
                        break;
                }

                string aResult = main.ValidateMessageType(msgHeader);
                Assert.AreEqual(e, aResult);
            }

        }


        [TestMethod]
        public void ValidateMessageTypeNullTest()
        {
            string msgID = "";

            Assert.ThrowsException<ArgumentNullException>(() => main.ValidateMessageType(msgID));
        }
        #endregion

        #region ValidateMessageLimit
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

        #region ValidateMessage
        [TestMethod]
        public void ValidateSignificantIncidentTest()
        {
            string msg = "Sender: liam.dickson@liam.co.uk\nSubject: SIR 12/12/12\nMessage Text: 12-34-56 Theft I was robbed";
            string msgType = "Email";
            string msgBody = "12-34-56 Theft I was robbed";
            string msgHeader = "E123456789";
            string msgSender = "liam.dickson@bsw.co.uk";

            Assert.IsTrue(main.ValidateMessage(msg, msgType, msgBody, msgHeader, msgSender));
        }

        [TestMethod]
        public void ValidateEmailTest()
        {
            string msg = "Sender: liam.dickson@liam.co.uk\nSubject: Hello\nMessage Text: This is a Test";
            string msgType = "Email";
            string msgBody = "This is a Test";
            string msgHeader = "E123456789";
            string msgSender = "liam.dickson@bsw.co.uk";

            Assert.IsTrue(main.ValidateMessage(msg, msgType, msgBody, msgHeader, msgSender));
        }

        [TestMethod]
        public void ValidateTweetTest()
        {
            string msg = "Sender: @Liam\nMessage Text: This is a Test";
            string msgType = "Tweet";
            string msgBody = "This is a Test";
            string msgHeader = "T123456789";
            string msgSender = "@Liam";

            Assert.IsTrue(main.ValidateMessage(msg, msgType, msgBody, msgHeader, msgSender));
        }

        [TestMethod]
        public void ValidateSMSTest()
        {
            string msg = "Sender: 07884969094\nMessage Text: This is a Test";
            string msgType = "SMS";
            string msgBody = "This is a Test";
            string msgHeader = "S123456789";
            string msgSender = "07884969094";

            Assert.IsTrue(main.ValidateMessage(msg, msgType, msgBody, msgHeader, msgSender));
        }

        [TestMethod]
        public void ValidateMessageNullTest()
        {
            string msg = "";
            string msgType = "";
            string msgBody = "";
            string msgHeader = "";
            string msgSender = "";

            Assert.ThrowsException<ArgumentNullException>(() => main.ValidateMessage(msg, msgType, msgBody, msgHeader, msgSender));
        }
        #endregion
    }
}