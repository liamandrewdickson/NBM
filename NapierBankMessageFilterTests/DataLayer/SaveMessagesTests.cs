using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using NapierBankMessageFilter.DataLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class SaveMessagesTests
    {
        #region GetIncidentType
        [TestMethod]
        public void GetIncidentTypeTest()
        {
            Message message = new Message("S123456789", "SMS", "This is a test", "07884969094");
            string msgType = "SMS";
            SaveMessages.SerializeMessage(message);

        }
        #endregion
    }
}