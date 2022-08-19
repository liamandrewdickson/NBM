using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using NapierBankMessageFilter.DataLayer;
using System;
using System.Collections.Generic;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class MessageTests
    {
        Message message = new Message();

        #region SetMessageTitles
        [TestMethod]
        public void SetMessageTitlesTest()
        {
            string[] msgType = { "Email", "Tweet" };
            string eResult = "Sender: \nSubject: \nMessage Text: ";

            foreach (string t in msgType)
            {
                if (t == "Tweet")
                {
                    eResult = "Sender: \nMessage Text: ";
                }

                string aResult = message.SetMessageTitles(t);
                Assert.AreEqual(eResult, aResult);
            }
        }

        [TestMethod]
        public void SetMessageTitlesNullTest()
        {
            string msgType = "";

            Assert.ThrowsException<ArgumentNullException>(() => message.SetMessageTitles(msgType));
        }
        #endregion

        #region GetMessageSender
        [TestMethod]
        public void GetMessageSenderTest()
        {
            string[] msgType = { "Email", "Tweet" };
            string msg = "Sender: liam.dickson@liam.co.uk\nSubject: This is a Test\nMessage Text: This is a Test";
            string eResult = "liam.dickson@liam.co.uk";

            foreach (string t in msgType)
            {
                if (t == "Tweet")
                {
                    msg = "Sender: @Liam\nMessage Text: This is a test";
                    eResult = "@Liam";
                }

                string aResult = message.GetMessageSender(t, msg);
                Assert.AreEqual(eResult, aResult);
            }
        }

        [TestMethod]
        public void GetMessageSenderNullTest()
        {
            string msgType = "";
            string msg = "";

            Assert.ThrowsException<ArgumentNullException>(() => message.GetMessageSender(msgType, msg));
        }

        #endregion

        #region GetMessageText
        [TestMethod]
        public void GetMessageTextTest()
        {
            string msg = "Sender: liam.dickson@liam.co.uk\nMessage Text: This is a Test";
            string eResult = "This is a Test";

            string aResult = message.GetMessageText(msg);
            Assert.AreEqual(eResult, aResult);
        }

        [TestMethod]
        public void GetMessageTextNullTest()
        {
            string msg = "";

            Assert.ThrowsException<ArgumentNullException>(() => message.GetMessageText(msg));
        }
        #endregion

        #region GetTextSpeak
        [TestMethod]
        public void GetTextSpeakTest()
        {
            string body = "AAP";
            Dictionary<string, string> initialisms = LoadMessages.LoadTextWords();
            string eResult = "AAP <Always a pleasure>";
            string aResult = message.GetTextSpeak(body, initialisms);

            Assert.AreEqual(eResult, aResult);
        }

        [TestMethod]
        public void GetTextSpeakNullTest()
        {
            string body = "";
            Dictionary<string, string> initialisms = LoadMessages.LoadTextWords();

            Assert.ThrowsException<ArgumentNullException>(() => message.GetTextSpeak(body, initialisms));
        }
        #endregion

    }
}