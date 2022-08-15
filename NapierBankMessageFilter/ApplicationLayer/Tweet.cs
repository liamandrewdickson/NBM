using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Tweet : Message
    {
        public void ValidateTweeter(string msg)
        {
            string[] msgParts;
            string[] delimiters = { "Sender: ", "\nMessage Text: " };
            msgParts = msg.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string tweeter = msgParts[1];
            Regex reg = new Regex(@"^(@){1}[1-9a-zA-Z]{15}");


            if (!string.IsNullOrEmpty(tweeter))
            {
                if (reg.IsMatch(tweeter))
                {
                    if (tweeter.Length > 20)
                    {
                        MessageBox.Show("There are too many characters in the sender of the email, please change the subject to fit the character limit of 20");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please do not pass Null values");
            }
        }
    }
}
