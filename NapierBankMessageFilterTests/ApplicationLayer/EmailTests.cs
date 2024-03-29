﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;
using System.Collections.Generic;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class EmailTests
    {
        Email email = new Email();

        #region GenerateEmail
        [TestMethod]
        public void GenerateEmailTest()
        {
            string msgHeader = "E123456789";
            string subject = "Hello";
            string msgType = "Email";
            string msgBody = "This is a test";
            string msgSender = "liam.dickson@liam.co.uk";
            List<string> urls = new List<string>();
            urls.Add("https://www.youtube.com/watch?v=dQw4w9WgXcQ");


            Email eResult = new Email("E123456789", "Hello", "Email", "This is a test", "liam.dickson@liam.co.uk", urls);

            Email aResult = new Email(msgHeader, subject, msgType, msgBody, msgSender, urls);

            Assert.AreEqual(eResult.Header, aResult.Header);
            Assert.AreEqual(eResult.Subject, aResult.Subject);
            Assert.AreEqual(eResult.Type, aResult.Type);
            Assert.AreEqual(eResult.Body, aResult.Body);
            Assert.AreEqual(eResult.Sender, aResult.Sender);
        }
        #endregion

        #region ValidateSubject
        [TestMethod]
        public void GetSubjectTest()
        {
            string msg = "Sender: liam.dickson@liam.co.uk \nSubject: ---------------------\nMessage Text: This is a test";
            string eResult = "";

            string aResult = email.GetSubject(msg);
            Assert.AreEqual(eResult, aResult);
        }

        #endregion
    }
}