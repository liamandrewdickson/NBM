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

        public SignificantIncident(string sortCode, string incidentType, string header, string subject, string type, string body, string sender) : base(header, subject, type, body, sender)
        {
            SortCode = sortCode;
            IncidentType = incidentType;
        }

        public SignificantIncident() { }

        /// <summary>
        /// Gets the Sort Code from the subject of the email
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>
        /// The Sort Code of the significant incident
        /// </returns>
        public string GetSortCode(string body)
        {
            Regex rx = new Regex(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b");

            MatchCollection matches = rx.Matches(body);

            foreach (Match match in matches)
            {
                SortCode = match.Value;
            }

            return SortCode;

        }

        /// <summary>
        /// Checks that the date is valid in the subject of the significant incident
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>
        /// The Sort Code of the significant incident
        /// </returns>
        public bool CheckDate(string subject)
        {
            bool valid = false;
            Regex rx = new Regex(@"^([0]?[1-9]|[1|2][0-9]|[3][0|1])[/]([0]?[1-9]|[1][0-2])[/]([0-9]{2})$");
            subject = subject.Remove(0, 4);

            MatchCollection matches = rx.Matches(subject);

            foreach (Match match in matches)
            {
                valid = true;
            }

            return valid;

        }

        public string GetIncident(string msg)
        {
            string body = "";

            int pFrom = msg.IndexOf("Subject: ") + "Subject: ".Length;
            int pTo = msg.LastIndexOf("\nMessage Text: ");
            string subject = msg.Substring(pFrom, pTo - pFrom);


            if (!string.IsNullOrEmpty(msg))

                    body = "Sender: \nMessage Text: ";
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }

            return body;
        }

        /// <summary>
        /// Gets the Incident Type from the subject of the email
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>
        /// The Incident Type of the significant incident
        /// </returns>
        public string GetIncidentType(string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                if (body.Contains("Theft") && !body.Contains("ATM"))
                {
                    IncidentType = "Theft";
                }
                else if (body.Contains("Staff Attack"))
                {
                    IncidentType = "Staff Attack";
                }
                else if(body.Contains("ATM Theft"))
                {
                    IncidentType = "ATM Theft";
                }
                else if(body.Contains("Raid"))
                {
                    IncidentType = "Raid";
                }
                else if (body.Contains("Customer Attack"))
                {
                    IncidentType = "Customer Attack";
                }
                else if (body.Contains("Staff Abuse"))
                {
                    IncidentType = "Staff Abuse";
                }
                else if(body.Contains("Bomb Threat"))
                {
                    IncidentType = "Bomb Threat";
                }
                else if(body.Contains("Terrorism"))
                {
                    IncidentType = "Terrorism";
                }
                else if(body.Contains("Suspicious Incident"))
                {
                    IncidentType = "Suspicious Incident";
                }
                else if(body.Contains("Intelligence"))
                {
                    IncidentType = "Intelligence";
                }
                else if(body.Contains("Cash Loss"))
                {
                    IncidentType = "Cash Loss";
                }
            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }

            return IncidentType;
        }


    }
}
