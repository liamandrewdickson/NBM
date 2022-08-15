using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class SMSTests
    {
        SMS sms = new SMS();

        #region ValidatePhoneNumber
        [TestMethod]
        public void ValidatePhoneNumberNullTest()
        {
            string msg = "Sender: \nMessage Text: This is a test";

            Assert.ThrowsException<ArgumentNullException>(() => sms.ValidatePhoneNumber(msg));
        }
        #endregion
    }
}