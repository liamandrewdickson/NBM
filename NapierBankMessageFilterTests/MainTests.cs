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

        #region GetMessageBody
        [TestMethod]
        public void GetMessageBodyTest()
        {
            string msgType = "Email";
            string eResult = "Sender: \nSubject: \nMessage Text: ";
            string aResult = main.GetMessageBody(msgType);

            Assert.AreEqual(eResult, aResult);
        }

        [TestMethod]
        public void GetMessageBodyNullTest()
        {
            string msgType = "";

            Assert.ThrowsException<ArgumentNullException>(() => main.GetMessageBody(msgType));
        }
        #endregion

        #region SetMessageLimit
        [TestMethod]
        public void SetMessageLimitNullTest()
        {
            string msgType = "Email";
            string msg = "Sender: liam.dickson@liam.co.uk \nSubject: This is a test \nMessage Text: ";

            Assert.ThrowsException<ArgumentNullException>(() => main.ValidateMessageLimit(msgType, msg));
        }
        #endregion
    }
}