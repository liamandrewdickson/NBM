using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Email : Message
    {
        private string _subject;

        public string Subject { get => _subject; set => _subject = value; }

        public Email(string header, string subject, string type, string body, string sender) : base(header, type, body, sender) 
        { 
            Subject = subject;
        }

        public Email() { }

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
                }
            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
            return subject;
        }

        ///// <summary>
        ///// This method takes the body added by the user and returns a list of the URLs detected
        ///// </summary>
        ///// <param name="body"></param>
        ///// <returns>
        ///// A list of URLs detected
        ///// </returns>
        //public List<string> DetectURL(string body)
        //{
        //    List<string> list;

        //    Regex rx = new(@"((http|ftp|https|HTTPS|HTTP|FTP):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");

        //    MatchCollection matches = rx.Matches(body);

        //    list = matches.Cast<Match>().Select(m => m.Value).ToList();

        //    return list;
        //}


        ///// <summary>
        ///// This method takes the body added by the user, and a list of URLs to be quarantined and removes the URLs from the body
        ///// </summary>
        ///// <param name="body"></param>
        ///// <param name="QuarantineList"></param>
        ///// <returns>
        ///// A new version of body with the URLs replaced with the text "URL Quarantined" in triangle brackets
        ///// </returns>
        //public string QuarantineURL(string body, List<string> QuarantineList)
        //{
        //    foreach (string URL in QuarantineList)
        //    {
        //        body = body.Replace(URL, "<URL Quarantined>");
        //    }

        //    return body;
        //}

    }
}
