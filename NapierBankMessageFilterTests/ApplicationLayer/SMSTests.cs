using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class SMSTests
    {
        SMS sms = new SMS();

        #region GenerateSMS
        [TestMethod]
        public void GenerateSMSTest()
        {
            string msgHeader = "S123456789";
            string msgType = "SMS";
            string msgBody = "This is a test";
            string msgSender = "07884969094";
            SMS eResult = new SMS("S123456789", "SMS", "This is a test", "07884969094");

            SMS aResult = new SMS(msgHeader, msgType, msgBody, msgSender);

            Assert.AreEqual(eResult.Header, aResult.Header);
            Assert.AreEqual(eResult.Type, aResult.Type);
            Assert.AreEqual(eResult.Body, aResult.Body);
            Assert.AreEqual(eResult.Sender, aResult.Sender);
        }
        #endregion

        #region ValidatePhoneNumber
        [TestMethod]
        public void ValidatePhoneNumberTest()
        {
            string phonenumber = "07884967095";
            bool aResult = sms.ValidatePhoneNumber(phonenumber);

            Assert.IsTrue(aResult);
        }

        [TestMethod]
        public void ValidatePhoneNumberLimitTest()
        {
            string phonenumber = "078849690944343242";
            bool aResult = sms.ValidatePhoneNumber(phonenumber);

            Assert.IsFalse(aResult);
        }

        [TestMethod]
        public void ValidatePhoneNumberExceptionalTest()
        {
            string phonenumber = "Test";
            bool aResult = sms.ValidatePhoneNumber(phonenumber);

            Assert.IsFalse(aResult);
        }
        #endregion
    }
}