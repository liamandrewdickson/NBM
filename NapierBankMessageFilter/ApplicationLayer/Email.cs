using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Email : Message
    {
        private string _subject;
        private List<string> _urls;

        public string Subject { get => _subject; set => _subject = value; }
        public List<string> URLs { get => _urls; set => _urls = value; }

        /// <summary>
        /// Creates a new Email
        /// </summary>
        /// <param name="header"></param>
        /// <param name="subject"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <param name="sender"></param>
        /// <param name="urls"></param>
        /// <returns>
        /// A Email with the parameters provided
        /// </returns>
        public Email(string header, string subject, string type, string body, string sender, List<string> urls) : base(header, type, body, sender) 
        { 
            Subject = subject;
            URLs = urls;
        }

        public Email() { }

        /// <summary>
        /// Splits the messsage to get the subject for the email
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>
        /// The subject for the email
        /// </returns>
        public string GetSubject(string msg)
        {
            int pFrom = msg.IndexOf("Subject: ") + "Subject: ".Length;
            int pTo = msg.LastIndexOf("\nMessage Text: ");
            string subject = msg.Substring(pFrom, pTo - pFrom);

            if (!string.IsNullOrEmpty(subject))
            {
                if (subject.Length > 20)
                {
                    MessageBox.Show("There are too many characters in the subject of the email, please change the subject to fit the character limit of 20");
                    subject = "";
                }
            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
            return subject;
        }

        /// <summary>
        /// Checks the body of the Email for URLs
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// A list of URLs
        /// </returns>
        public List<string> CheckURLs(string body)
        {
            List<string> list = new List<string>();
            Regex reg = new(@"((http|ftp|https|HTTPS|HTTP|FTP):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");
            MatchCollection matches = reg.Matches(body);

            list = matches.Cast<Match>().Select(m => m.Value).ToList();

            return list;
        }


        /// <summary>chrome://vivaldi-webui/startpage?section=Speed-dials&background-color=#2f2f2f
        /// Quarantines URLs in the body of the Email passed to the method
        /// </summary>
        /// <param name="body"></param>
        /// <param name="QuarantineURLs"></param>
        /// <returns>
        /// The body of the message with the URLs replaced with URL Quarantined
        /// </returns>
        public string Quarantine(string body, List<string> quarantineURLs)
        {
            foreach (string url in quarantineURLs)
            {
                body = body.Replace(url, "<URL Quarantined>");
            }

            return body;
        }


        /// <summary>
        /// Gets the URLs from the passed Emails
        /// </summary>
        /// <param name="emails"></param>
        /// <returns>
        /// A List of URLs 
        /// </returns>
        public static List<string> CollectURLs(List<Message> emails)
        {
            List<string> list = new List<string>();

            foreach (Message message in emails)
            {
                Email email = (Email)message;

                if (email.URLs != null)
                {
                    foreach (string url in email.URLs)
                    {
                        list.Add(url);
                    }
                }

            }

            return list;
        }

    }
}
