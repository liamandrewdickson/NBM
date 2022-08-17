using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
