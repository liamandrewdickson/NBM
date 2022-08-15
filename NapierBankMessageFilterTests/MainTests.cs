using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class MainTests
    {
        [TestMethod]
        public void ValidateMessageTypeTest()
        {
            Main main = new Main();
            string msgID = "E123456789";
            string eResult = "Email";
            string aResult = main.ValidateMessageType(msgID);

            Assert.AreEqual(eResult, aResult);
        }
    }
}