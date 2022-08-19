using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using NapierBankMessageFilter.DataLayer;
using System;
using System.Collections.Generic;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class LoadMessagesTests
    {

        #region LoadTextWords
        [TestMethod]
        public void LoadTextWordsTest()
        {
            Dictionary<string, string> initialisms = LoadMessages.LoadTextWords();
            Dictionary<string, string> eInitialisms = LoadMessages.LoadTextWords();

            CollectionAssert.AreEqual(initialisms, eInitialisms);
        }
        #endregion

        #region DeserializeMessage
        [TestMethod]
        public void DeserializeMessageTest()
        {
            LoadMessages.InitialiseLocations();
            Message message = new Message("S123456789", "SMS", "This is a test", "07884969094");
            string[] msgType = { "SMS", "Email", "Tweet" };

            foreach (string type in msgType)
            {
                switch (type)
                {
                    case "Tweet":
                        message = new Message("T123456789", "Tweet", "This is a test", "@Liam");
                        break;
                    case "Email":
                        message = new Email("E123456789", "Hello Subject", "Email", "This is a test", "liam.dickson@liam.co.uk");
                        break;
                }
                SaveMessages.SerializeMessage(message);

                List<Message> list = LoadMessages.DeserializeMessages(type);
                foreach (Message t in list)
                {
                    Assert.AreEqual(message.Header, t.Header);
                    Assert.AreEqual(message.Type, t.Type);
                    Assert.AreEqual(message.Body, t.Body);
                    Assert.AreEqual(message.Sender, t.Sender);
                }
            }

        }
        #endregion
    }
}