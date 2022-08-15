using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class EmailTests
    {
        Email email = new Email();

        #region ValidateSubject
        [TestMethod]
        public void ValidateSubjectNullTest()
        {
            string msg = "Sender: liam.dickson@liam.co.uk \nSubject: \nMessage Text: This is a test";

            Assert.ThrowsException<ArgumentNullException>(() => email.ValidateSubject(msg));
        }
        #endregion
    }
}