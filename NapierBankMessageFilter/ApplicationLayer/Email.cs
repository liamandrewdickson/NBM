using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Email : Message
    {
        public void ValidateSubject(string msg)
        {
            string[] msgParts;
            string[] delimiters = { "Subject: ", "\nMessage Text: " };
            msgParts = msg.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string subject = msgParts[1];
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
        }
    }
}
