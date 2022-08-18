using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class SignificantIncident : Email
    {
        private string _sortCode;
        private string _significantIncidentType;

        public string SortCode { get => _sortCode; set => _sortCode = value; }
        public string IncidentType { get => _significantIncidentType; set => _significantIncidentType = value; }


        public string GetSortCode(string subject)
        {
            Regex rx = new Regex(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b");

            MatchCollection matches = rx.Matches(subject);

            foreach (Match match in matches)
            {
                SortCode = match.Value;
            }

            return SortCode;

        }

        public string GetIncidentType(string subject)
        {
            switch (subject)
            {
                case "Theft":
                    IncidentType = "Theft";
                    break;
                case "Staff Attack":
                    IncidentType = "Staff Attack";
                    break;
                case "ATM Theft":
                    IncidentType = "ATM Theft";
                    break;
                case "Raid":
                    IncidentType = "Raid";
                    break;
                case "Customer Attack":
                    IncidentType = "Customer Attack";
                    break;
                case "Staff Abuse":
                    IncidentType = "Staff Abuse";
                    break;
                case "Bomb Threat":
                    IncidentType = "Bomb Threat";
                    break;
                case "Terrorism":
                    IncidentType = "Terrorism";
                    break;
                case "Suspicious Incident":
                    IncidentType = "Suspicious Incident";
                    break;
                case "Intelligence":
                    IncidentType = "Intelligence";
                    break;
                case "Cash Loss":
                    IncidentType = "Cash Loss";
                    break;
            }

            return IncidentType;
        }


    }
}
