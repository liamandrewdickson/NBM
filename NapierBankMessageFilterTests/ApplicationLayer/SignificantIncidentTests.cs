using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilter.ApplicationLayer;
using System;

namespace NapierBankMessageFilterTests
{
    [TestClass]
    public class SignificantIncidentTests
    {
        SignificantIncident significantIncident = new SignificantIncident();

        #region GetIncidentType
        [TestMethod]
        public void GetIncidentTypeTest()
        {
            string[] incidentTypes = { "Theft", "Staff Attack", "ATM Theft", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism", "Suspicious Incident", "Intelligence", "Cash Loss" };

            foreach (string incidentType in incidentTypes)
            {
                string aResult = significantIncident.GetIncidentType(incidentType);
                Assert.AreEqual(incidentType, aResult);
            }

        }

        [TestMethod]
        public void GetIncidentTypeNullTest()
        {
            string subject = "";
            Assert.ThrowsException<ArgumentNullException>(() => significantIncident.GetIncidentType(subject));
        }
        #endregion

    }
}