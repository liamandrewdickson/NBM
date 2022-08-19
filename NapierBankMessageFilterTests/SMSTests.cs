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
        public void ValidatePhoneNumberTest()
        {
            string phonenumber = "07884969094";
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

        [TestMethod]
        public void ValidatePhoneNumberNullTest()
        {
            string phonenumber = "";

            Assert.ThrowsException<ArgumentNullException>(() => sms.ValidatePhoneNumber(phonenumber));
        }
        #endregion
    }
}